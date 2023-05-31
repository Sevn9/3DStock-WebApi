using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Commands.CreateModel3D
{
  public class CreateModel3DCommand : IRequest<Guid>
  {
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public string? Category { get; set; }

    public string? Price { get; set; }
  }
}
