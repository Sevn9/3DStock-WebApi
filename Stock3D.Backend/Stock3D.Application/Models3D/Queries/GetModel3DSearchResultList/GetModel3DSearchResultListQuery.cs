using MediatR;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DSearchResultList
{
  public class GetModel3DSearchResultListQuery: IRequest<Model3DResultListVm>
  {
    public Guid UserId { get; set; }
    public string SearchString { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
  }
}
