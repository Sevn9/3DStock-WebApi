using MediatR;
using Stock3D.Application.Common.Exceptions;
using Stock3D.Application.Interfaces;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Commands.DeleteModel3D
{
  public class DeleteModel3DCommandHandler : IRequestHandler<DeleteModel3DCommand>
  {
    private readonly IStock3DDbContext _dbContext;

    public DeleteModel3DCommandHandler(IStock3DDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(DeleteModel3DCommand request, CancellationToken cancellationToken)
    {
      //выполняем поиск, если не найдено то бросаем исключение
      var entity = await _dbContext.Models3D
        .FindAsync(new object[] { request.Id }, cancellationToken);

      if (entity == null || entity.UserId != request.UserId)
      {
        throw new NotFoundException(nameof(Model3D), request.Id);
      }
      // если нашли то выполняем изменения
      _dbContext.Models3D.Remove(entity);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
