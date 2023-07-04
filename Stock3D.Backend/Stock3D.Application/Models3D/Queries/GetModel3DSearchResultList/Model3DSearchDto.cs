using AutoMapper;
using Stock3D.Application.Common.Mappings;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Models3D.Queries.GetModel3DSearchResultList
{
  public class Model3DSearchDto : IMapWith<Model3D>
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string? Category { get; set; }
    public string? Price { get; set; }

    public string? FileFormat { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? FilePath { get; set; }
    public string? ImagePath { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<Model3D, Model3DSearchDto>()
        .ForMember(model3DDto => model3DDto.Id,
        opt => opt.MapFrom(model3DDto => model3DDto.Id))
        .ForMember(model3DDto => model3DDto.Title,
        opt => opt.MapFrom(model3DDto => model3DDto.Title))
        .ForMember(model3DCommand => model3DCommand.Details,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Details))
        .ForMember(model3DCommand => model3DCommand.Category,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Category))
        .ForMember(model3DCommand => model3DCommand.Price,
        opt => opt.MapFrom(model3DCommand => model3DCommand.Price))
        .ForMember(model3DCommand => model3DCommand.FileFormat,
        opt => opt.MapFrom(model3DCommand => model3DCommand.FileFormat))
        .ForMember(model3DCommand => model3DCommand.UploadDate,
        opt => opt.MapFrom(model3DCommand => model3DCommand.UploadDate))
        .ForMember(model3DCommand => model3DCommand.FilePath,
        opt => opt.MapFrom(model3DCommand => model3DCommand.FilePath))
        .ForMember(model3DCommand => model3DCommand.ImagePath,
        opt => opt.MapFrom(model3DCommand => model3DCommand.ImagePath));
    }

  }
}
