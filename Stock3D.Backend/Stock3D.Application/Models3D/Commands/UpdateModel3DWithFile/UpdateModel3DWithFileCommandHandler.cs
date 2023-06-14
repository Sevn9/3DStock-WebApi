using Amazon.S3;
using Amazon.S3.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Common.Exceptions;
using Stock3D.Application.Interfaces;
using Stock3D.CloudStorage;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Commands.UpdateModel3DWithFile
{
  public class UpdateModel3DWithFileCommandHandler : IRequestHandler<UpdateModel3DWithFileCommand>
  {
    private readonly IStock3DDbContext _dbContext;

    //конструктор
    public UpdateModel3DWithFileCommandHandler(IStock3DDbContext dbContext, CloudStorageAuth cloudStorageAuth) 
    {
      _dbContext = dbContext;
      _cloudStorageAuth = cloudStorageAuth;
      data = _cloudStorageAuth.GetClientData();
    }

    private readonly CloudStorageAuth _cloudStorageAuth;
    private ClientData data;

    public async Task<Unit> Handle(UpdateModel3DWithFileCommand request, CancellationToken cancellationToken)
    {
      //выполняем поиск сущностей по id, если сущность не найдена или id пользователя
      //не совпадает с id пользователя в запросе то будем выдавать исключения
      var entity =
        await _dbContext.Models3D.FirstOrDefaultAsync(model3D =>
        model3D.Id == request.Id, cancellationToken);

      if (entity == null || entity.UserId != request.UserId)
      {
        throw new NotFoundException(nameof(Model3D), request.Id);

      }
      //изменяем данные
      entity.Details = request.Details;
      entity.Title = request.Title;
      //entity.UploadDate = DateTime.Now;
      entity.Price = request.Price;
      entity.Category = request.Category;

      //entity.FilePath = request.FilePath;
      //entity.FileFormat = request.FileFormat;

      await _dbContext.SaveChangesAsync(cancellationToken);



      return Unit.Value;
    }

  }
}
