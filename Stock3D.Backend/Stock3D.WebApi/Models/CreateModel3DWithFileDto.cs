using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Models3D.Commands.CreateModel3D;
using Stock3D.Application.Models3D.Commands.CreateModel3DWithFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Models
{
  public class CreateModel3DWithFileDto : IMapWith<CreateModel3DWithFileCommand>
  {
    //[Required] этот атрибут изменяет поведение пользовательского интерфейса и схемы базового json
    [Required]
    public string Title { get; set; }
    public string Details { get; set; }

    public string? Category { get; set; }

    public string? Price { get; set; }

    public IFormFile? File { get; set; }
    public IFormFile Image { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<CreateModel3DWithFileDto, CreateModel3DWithFileCommand>()
                .ForMember(model3DCommand => model3DCommand.Title,
                    opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(model3DCommand => model3DCommand.Details,
                    opt => opt.MapFrom(model3DCommand => model3DCommand.Details))
                .ForMember(model3DCommand => model3DCommand.File,
                opt => opt.MapFrom(model3DCommand => model3DCommand.File))
                .ForMember(model3DCommand => model3DCommand.Category,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Category))
                .ForMember(model3DCommand => model3DCommand.Price,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Price))
                .ForMember(model3DCommand => model3DCommand.Image,
                opt => opt.MapFrom(model3DCommand => model3DCommand.Image));
                
    }

  }
}
