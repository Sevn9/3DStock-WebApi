using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Common.Mappings
{
  //реализация интерфейса по умолчанию
  public interface IMapWith<T>
  {
    void Mapping(Profile profile) =>
     profile.CreateMap(typeof(T), GetType());
  }
}
