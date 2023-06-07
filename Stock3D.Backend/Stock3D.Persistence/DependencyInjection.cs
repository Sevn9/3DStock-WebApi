using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock3D.Application.Interfaces;
using System;

namespace Stock3D.Persistence
{
  public static class DependencyInjection
  {
    //метод расширения для добавления контекста базы данных в веб приложение
    public static IServiceCollection AddPersistence(this IServiceCollection services,
      IConfiguration configuration)
    {
      //добавляем контекст базы данных и регистрируем его
      var connectionString = configuration["DbConnection"];
      services.AddDbContext<Stock3DDbContext>(options =>
      {
        options.UseNpgsql(connectionString);
      });
      services.AddScoped<IStock3DDbContext>(provider => provider.GetService<Stock3DDbContext>());
      return services;
    }

  }
}
