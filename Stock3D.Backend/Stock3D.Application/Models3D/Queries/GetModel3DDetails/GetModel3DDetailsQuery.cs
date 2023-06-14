using MediatR;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DDetails
{
  public class GetModel3DDetailsQuery : IRequest<Model3DDetailsVm>
  {
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

  }
}
