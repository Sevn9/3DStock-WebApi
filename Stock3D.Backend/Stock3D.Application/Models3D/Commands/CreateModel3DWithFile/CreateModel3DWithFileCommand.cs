using MediatR;
using Microsoft.AspNetCore.Http;
using System;

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

  }
}
