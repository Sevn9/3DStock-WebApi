﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]/[action]")]
  public abstract class BaseController: ControllerBase
  {
    //для формирования комманд для выполнения запросов
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    internal Guid UserId => !User.Identity.IsAuthenticated
      ? Guid.Empty
      : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


  }
}
