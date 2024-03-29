﻿using Microsoft.AspNetCore.Mvc;
using Stock3D.Application.Models3D.Queries.GetModel3DSearchResultList;
using Stock3D.WebApi.Models;
using System;

namespace Stock3D.WebApi.Controllers
{
  //Produces - действия контроллера поддерживают тип содержимого ответа application/json
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class SearchModel3DController : BaseController
  {
    // Поиск 3д моделей
    /// <summary>
    /// searching 3D models
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Model3DResultListVm>> Search(string? searchString, [FromQuery] PageParameters pageParameters)
    {
      var query = new GetModel3DSearchResultListQuery
      {
        UserId = UserId,
        SearchString = searchString,
        PageNumber = pageParameters.PageNumber,
        PageSize = pageParameters.PageSize,
      };
      var vm = await Mediator.Send(query);
      return Ok(vm);
    }


  }
}
