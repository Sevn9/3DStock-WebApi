using MediatR;
using System;

namespace Stock3D.Application.Models3D.Commands.UpdateModel3D
{
  public class UpdateModel3DCommand : IRequest
  {
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string? Category { get; set; }
    public string? Price { get; set; }

  }
}
