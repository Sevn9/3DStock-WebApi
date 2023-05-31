﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Common.Exceptions;
using Stock3D.Application.Interfaces;
using Stock3D.Application.Models3D.Queries.GetModel3DList;
using Stock3D.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stock3D.Application.Models3D.Queries.GetModel3DSearchResultList
{
  public class GetModel3DSearchResultListQueryHandler : IRequestHandler<GetModel3DSearchResultListQuery, Model3DResultListVm>
  {
    private readonly IStock3DDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetModel3DSearchResultListQueryHandler(IStock3DDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task<Model3DResultListVm> Handle(GetModel3DSearchResultListQuery request, CancellationToken cancellationToken)
    {

      //ProjectTo метод расширения библиотеки автомапер который проецирует нашу коллекцию
      // в соответствие в заданной конфигурации
      var models3DQuery = await _dbContext.Models3D
        .Where(model3D => model3D.UserId == request.UserId && (model3D.Title.ToLower().Contains(request.SearchString.ToLower()) 
        || model3D.Category.ToLower().Contains(request.SearchString.ToLower()) 
        || model3D.Details.ToLower().Contains(request.SearchString.ToLower())) )
        .ProjectTo<Model3DSearchDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

        return new Model3DResultListVm { Models3D = models3DQuery };
      
    }

  }
}