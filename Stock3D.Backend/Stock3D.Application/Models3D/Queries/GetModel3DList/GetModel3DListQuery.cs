using MediatR;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DList
{
  public class GetModel3DListQuery: IRequest<Model3DListVm>
  {
    public Guid UserId { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }


  }
}
