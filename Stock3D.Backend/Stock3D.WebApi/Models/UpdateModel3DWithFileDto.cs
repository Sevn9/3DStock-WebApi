using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Models3D.Commands.CreateModel3DWithFile;
using Stock3D.Application.Models3D.Commands.UpdateModel3DWithFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Models
{
  public class UpdateModel3DWithFileDto : IMapWith<UpdateModel3DWithFileCommand>
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public string? Category { get; set; }

    public string? Price { get; set; }

    //public IFormFile? File { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<UpdateModel3DWithFileDto, UpdateModel3DWithFileCommand>()
                .ForMember(model3DCommand => model3DCommand.Id,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Id))
                .ForMember(model3DCommand => model3DCommand.Title,
                opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(model3DCommand => model3DCommand.Details,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Details))
                .ForMember(model3DCommand => model3DCommand.Category,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Category))
                .ForMember(model3DCommand => model3DCommand.Price,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Price));

    }

  }
}
