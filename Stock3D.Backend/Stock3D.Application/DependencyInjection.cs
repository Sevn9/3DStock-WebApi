//using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;


namespace Stock3D.Application
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddMediatR(Assembly.GetExecutingAssembly());
      // добавляем валидацию
      //services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
      // регистрирем пайплайн бехавиор
      //services.AddTransient(typeof(IPipelineBehavior<,>),
      //  typeof(ValidationBehavior<,>));
      return services;
    }

  }
}
