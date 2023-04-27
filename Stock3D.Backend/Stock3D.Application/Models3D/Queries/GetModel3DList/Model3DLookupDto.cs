using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Queries.GetModel3DList
{
  //запрос и получение списка моделей
  //здесь только та информация которая списку моделей необходима (это данные, а с суффиком vm это представление)
  public class Model3DLookupDto: IMapWith<Model3D>
  {
    public Guid Id { get; set; }
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<Model3D, Model3DLookupDto>()
        .ForMember(model3DDto => model3DDto.Id,
        opt => opt.MapFrom(model3DDto => model3DDto.Id))
        .ForMember(model3DDto => model3DDto.Title,
        opt => opt.MapFrom(model3DDto => model3DDto.Title));
    }

  }
}
