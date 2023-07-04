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
      
      //создаем запрос к облачному хранилищу для 3d объекта
      var objectFileRequest = new PutObjectRequest()
      {
        BucketName = data.CloudSettings.BucketName,
        Key = $"{request.UserId}/Object/{request.File.FileName}",
        InputStream = request.File.OpenReadStream(),

      };
      //кладем 3d объект в хранилище
      var responseFileRequest = await data.Client.PutObjectAsync(objectFileRequest);

      string fileFullName = $"{request.UserId}/Object/{request.File.FileName}";
      string fileUrl = $"{data.CloudSettings.ServiceURL}/{data.CloudSettings.BucketName}/{request.UserId}/Object/{request.File.FileName}";
      string fileFormat = Path.GetExtension(request.File.FileName);
      
      //создаем запрос к облачному хранилищу для картинки
      var objectImageRequest = new PutObjectRequest()
      {
        BucketName = data.CloudSettings.BucketName,
        Key = $"{request.UserId}/Image/{request.Image.FileName}",
        InputStream = request.Image.OpenReadStream(),

      };
      //кладем картинку в хранилище
      var responseImageRequest = await data.Client.PutObjectAsync(objectImageRequest);

      string ImageUrl = $"{data.CloudSettings.ServiceURL}/{data.CloudSettings.BucketName}/{request.UserId}/Image/{request.Image.FileName}";
      //формируем модель из нашего запроса и возвращаем id созданной модели
      var model3D = new Model3D
      {
        Id = Guid.NewGuid(),
        UserId = request.UserId,
        Title = request.Title,
        Details = request.Details,
        Category = request.Category,
        FileFormat= fileFormat,
        Price= request.Price,
        FilePath = fileUrl,
        ImagePath = ImageUrl,
        UploadDate = DateTime.UtcNow,
        FileFullName= fileFullName,

      };

      // добавление созданной модели в контекст, а затем сохранение изменений в базу
      await _dbContext.Models3D.AddAsync(model3D, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return model3D.Id;

    }
  }
}
