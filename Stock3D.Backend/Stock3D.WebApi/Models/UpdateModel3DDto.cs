using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Models3D.Commands.UpdateModel3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Models
{
  public class UpdateModel3DDto : IMapWith<UpdateModel3DCommand>
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<UpdateModel3DDto, UpdateModel3DCommand>()
        .ForMember(model3DCommand => model3DCommand.Id,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Id))
        .ForMember(model3DCommand => model3DCommand.Title,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Title))
        .ForMember(model3DCommand => model3DCommand.Details,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Details));
    }

  }
}
