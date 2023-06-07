using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Commands.CreateModel3DWithFile
{
  public class CreateModel3DWithFileCommand : IRequest<Guid>
  {
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public IFormFile File { get; set; }
    public DateTime? UploadDate { get; set; }

    public string? FilePath { get; set; }

    //public File file { get; set; }



  }
}
