using MediatR;
using Stock3D.Application.Models3D.Queries.GetModel3DDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Queries.GetModel3DSearchResultList
{
  public class GetModel3DSearchResultListQuery: IRequest<Model3DResultListVm>
  {
    public Guid UserId { get; set; }
    public string SearchString { get; set; }
  }
}
