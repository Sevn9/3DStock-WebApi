using AutoMapper;
using System.Reflection;

namespace Stock3D.Application.Common.Mappings
{
  public class AssemblyMappingProfile : Profile
  {
    //конструктор с параметром assembly, где assembly это наша сборка
    public AssemblyMappingProfile(Assembly assembly) =>
      ApplyMappingsFromAssembly(assembly);

    //метод сканирует сборку и ищет любые типы реализующие IMapWith
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
      var types = assembly.GetExportedTypes().Where(type => type.GetInterfaces()
      .Any(i => i.IsGenericType &&
      i.GetGenericTypeDefinition() == typeof(IMapWith<>))).ToList();

      foreach (var type in types)
      {
        var instance = Activator.CreateInstance(type);
        var methodInfo = type.GetMethod("Mapping");
        methodInfo?.Invoke(instance, new object[] { this });
      }
    }

  }
}
