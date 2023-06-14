using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Domain;
using System;


namespace Stock3D.Application.Models3D.Queries.GetModel3DDetails
{
  //Model3DDetailsVm это вью модель, класс который описывает то что будет возвращаться пользователю,
  //когда он будет запрашивать детали заметки
  public class Model3DDetailsVm : IMapWith<Model3D>
  {
    //id самой модели
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime UploadDate { get; set; }

    //создает соответствие между классом Model3D и Model3DDetailsVm
    public void Mapping(Profile profile)
    {
      profile.CreateMap<Model3D, Model3DDetailsVm>()
        .ForMember(model3DVm => model3DVm.Title,
        opt => opt.MapFrom(model3DVm => model3DVm.Title))
        .ForMember(model3DVm => model3DVm.Details,
        opt => opt.MapFrom(model3DVm => model3DVm.Details))
        .ForMember(model3DVm => model3DVm.Id,
        opt => opt.MapFrom(model3DVm => model3DVm.Id))
        .ForMember(model3DVm => model3DVm.UploadDate,
        opt => opt.MapFrom(model3DVm => model3DVm.UploadDate));

    }

  }
}
