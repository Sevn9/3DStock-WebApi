using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Models3D.Commands.CreateModel3D;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Models
{
  //с клиента будут приходить данные о создаваемой модели причем клиенту не нужно знать свой 
  //id пользователя
  //смапим ее с командой создания модели
  public class CreateModel3DDto : IMapWith<CreateModel3DCommand>
  {
    //[Required] этот атрибут изменяет поведение пользовательского интерфейса и схемы базового json
    [Required]
    public string Title { get; set; }
    public string Details { get; set; }
    public string? Category { get; set; }
    public string? Price { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<CreateModel3DDto, CreateModel3DCommand>()
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
