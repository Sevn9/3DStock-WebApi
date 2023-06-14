using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Commands.UpdateModel3DWithFile
{
  public class UpdateModel3DWithFileCommand: IRequest
  {
    public Guid UserId { get; set; }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public string? Category { get; set; }

    //public string? FileFormat { get; set; }
    public string? Price { get; set; }

    //public IFormFile File { get; set; }
    //public DateTime? UploadDate { get; set; }

    //public string? FilePath { get; set; }
  }
}
