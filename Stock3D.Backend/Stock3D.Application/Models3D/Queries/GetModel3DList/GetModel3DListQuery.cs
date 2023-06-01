using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Queries.GetModel3DList
{
  public class GetModel3DListQuery: IRequest<Model3DListVm>
  {
    public Guid UserId { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }


  }
}
