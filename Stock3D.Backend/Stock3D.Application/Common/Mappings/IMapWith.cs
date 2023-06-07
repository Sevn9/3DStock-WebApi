using AutoMapper;
using System;

namespace Stock3D.Application.Common.Mappings
{
  //реализация интерфейса по умолчанию
  public interface IMapWith<T>
  {
    void Mapping(Profile profile) =>
     profile.CreateMap(typeof(T), GetType());
  }
}
