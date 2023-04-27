using MediatR;
using System;

namespace Stock3D.Application.Models3D.Commands.DeleteModel3D
{
  public class DeleteModel3DCommand : IRequest
  {
    public Guid UserId { get; set; }
    //id модели
    public Guid Id { get; set; }

  }
}
