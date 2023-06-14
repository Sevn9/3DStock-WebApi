using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Interfaces;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DList
{
  public class GetModel3DListQueryHandler : IRequestHandler<GetModel3DListQuery, Model3DListVm>
  {
    private readonly IStock3DDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetModel3DListQueryHandler(IStock3DDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }
    public async Task<Model3DListVm> Handle(GetModel3DListQuery request, CancellationToken cancellationToken)
    {
      //ProjectTo метод расширения библиотеки автомапер который проецирует нашу коллекцию
      // в соответствие c заданной конфигурацией
      var models3DQuery = await _dbContext.Models3D
        .Where(model3D => model3D.UserId == request.UserId)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ProjectTo<Model3DLookupDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

      return new Model3DListVm { Models3D = models3DQuery };
    }

  }
}
