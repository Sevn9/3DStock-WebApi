﻿using MediatR;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DDetails
{
  //Model3DDetailsVm это вью модель, класс который описывает то что будет возвращаться пользователю,
  //когда он будет запрашивать детали заметки
  public class GetModel3DDetailsQuery : IRequest<Model3DDetailsVm>
  {
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

  }
}
