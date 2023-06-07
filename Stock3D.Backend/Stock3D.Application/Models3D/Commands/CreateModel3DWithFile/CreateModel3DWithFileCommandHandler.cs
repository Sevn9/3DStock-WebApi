using Amazon.S3;
using Amazon.S3.Model;
using MediatR;
using Stock3D.Application.Interfaces;
using Stock3D.CloudStorage;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Commands.CreateModel3DWithFile
{
  public class CreateModel3DWithFileCommandHandler : IRequestHandler<CreateModel3DWithFileCommand, Guid>
  {
    //сохранение изменений в базу
    private readonly IStock3DDbContext _dbContext;
    private readonly CloudStorageAuth _cloudStorageAuth;
    private ClientData data;
    //сделаем внедрение зависимости на контекст базы данных через конструктор
    public CreateModel3DWithFileCommandHandler(IStock3DDbContext dbContext, CloudStorageAuth cloudStorageAuth)
    {
      _dbContext = dbContext;
      _cloudStorageAuth = cloudStorageAuth;
      data = _cloudStorageAuth.GetClientData();
    }

    //логика обработки команд находится в handle
    public async Task<Guid> Handle(CreateModel3DWithFileCommand request, CancellationToken cancellationToken)
    {
      //создаем запрос к облачному хранилищу
      var objectRequest = new PutObjectRequest()
      {
        BucketName = data.CloudSettings.BucketName,
        Key = $"{request.UserId}/{request.File.FileName}",
        InputStream = request.File.OpenReadStream(),

      };
      var response = await data.Client.PutObjectAsync(objectRequest);

      //получаем ссылку на загруженный объект
      var preSignedUrlRequest = new GetPreSignedUrlRequest
      {
        BucketName = data.CloudSettings.BucketName,
        Key = $"{request.UserId}/{request.File.FileName}",
        Verb = HttpVerb.PUT,
        Expires = DateTime.UtcNow.AddSeconds(30),
      };

      string url = data.Client.GetPreSignedURL(preSignedUrlRequest);

      //формируем модель из нашего запроса и возвращаем id созданной модели
      var model3D = new Model3D
      {
        UserId = request.UserId,
        Title = request.Title,
        Details = request.Details,
        FilePath = url,
        Id = Guid.NewGuid(),
        UploadDate = DateTime.UtcNow,
      };

      // добавление созданной модели в контекст, а затем сохранение изменений в базу
      await _dbContext.Models3D.AddAsync(model3D, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return model3D.Id;

    }
  }
}
