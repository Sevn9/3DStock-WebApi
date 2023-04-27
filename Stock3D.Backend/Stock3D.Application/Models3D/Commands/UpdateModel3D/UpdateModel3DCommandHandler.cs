using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Common.Exceptions;
using Stock3D.Application.Interfaces;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Commands.UpdateModel3D
{
  public class UpdateModel3DCommandHandler : IRequestHandler<UpdateModel3DCommand>
  {
    private readonly IStock3DDbContext _dbContext;

    //конструктор
    public UpdateModel3DCommandHandler(IStock3DDbContext dbContext) =>
      _dbContext = dbContext;

    public async Task<Unit> Handle(UpdateModel3DCommand request, CancellationToken cancellationToken)
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

      entity.Details = request.Details;
      entity.Title = request.Title;
      entity.UploadDate = DateTime.Now;

      await _dbContext.SaveChangesAsync(cancellationToken);



      return Unit.Value;
    }
  }
}
