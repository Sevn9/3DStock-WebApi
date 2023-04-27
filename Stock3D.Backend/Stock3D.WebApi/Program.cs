using Microsoft.Extensions.Options;
using Stock3D.Application;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Interfaces;
using Stock3D.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
  
  services.AddAutoMapper(config =>
  {
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IStock3DDbContext).Assembly));
  });
  
  services.AddApplication();
  services.AddPersistence(builder.Configuration);

  services.AddControllers();

  //для безопасности
  services.AddCors(options =>
  {
    options.AddPolicy("AllowAll", policy =>
    {
      policy.AllowAnyHeader();
      policy.AllowAnyMethod();
      policy.AllowAnyOrigin();
    });
  });
 
  services.AddSwaggerGen(config =>
  {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
  });
  services.AddSwaggerGen();

}
//настройка пайплайна(конвейера) указываем что будет использовать приложение здесь
//сюда же подключается мидлваре


void Configure(WebApplication app)
{
  if (app.Environment.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }
  app.UseSwagger();
  app.UseSwaggerUI(
    config =>
    {
      config.RoutePrefix = string.Empty;
      config.SwaggerEndpoint("swagger/v1/swagger.json", "Stock3D API");
    }
    );
  
  using var scope = app.Services.CreateScope();
  //вызываем метод инициализации базы здесь
  var serviceProvider = scope.ServiceProvider;
  //получаем serviceProvider использующийся для разрешения зависимостей и получить контекст
  var context = serviceProvider.GetRequiredService<Stock3DDbContext>();// for accessing dependencies
                                                                     //вызываем метод инициализации базы и передаем контекст
  DbInitializer.Initialize(context);


  app.UseRouting();
  app.UseHttpsRedirection();
  app.UseCors("AllowAll");


  app.UseEndpoints(endpoints =>
  {
    //роутинг будет мапиться на названия контроллеров и их методы
    endpoints.MapControllers();
  });
}