using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Common.Exceptions;
using Stock3D.Application.Interfaces;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DDetails
{
  //обработчик, делаем так же как для комманд только еще нужен будет маппер чтобы полученную из базы
  //сущность смапить на возвращаемый тип и вернуть объект типа Model3DDetailsVm
  public class GetModel3DDetailsQueryHandler : IRequestHandler<GetModel3DDetailsQuery, Model3DDetailsVm>
  {
    private readonly IStock3DDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetModel3DDetailsQueryHandler(IStock3DDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task<Model3DDetailsVm> Handle(GetModel3DDetailsQuery request,
      CancellationToken cancellationToken)
    {
      var entity = await _dbContext.Models3D
        .FirstOrDefaultAsync(model3D => model3D.Id == request.Id, cancellationToken);
      if (entity == null)
      {
        throw new NotFoundException(nameof(Model3D), request.Id);
      }

      return _mapper.Map<Model3DDetailsVm>(entity);

    }

  }
}
