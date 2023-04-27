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

  //��� ������������
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
//��������� ���������(���������) ��������� ��� ����� ������������ ���������� �����
//���� �� ������������ ��������


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
  //�������� ����� ������������� ���� �����
  var serviceProvider = scope.ServiceProvider;
  //�������� serviceProvider �������������� ��� ���������� ������������ � �������� ��������
  var context = serviceProvider.GetRequiredService<Stock3DDbContext>();// for accessing dependencies
                                                                     //�������� ����� ������������� ���� � �������� ��������
  DbInitializer.Initialize(context);


  app.UseRouting();
  app.UseHttpsRedirection();
  app.UseCors("AllowAll");


  app.UseEndpoints(endpoints =>
  {
    //������� ����� �������� �� �������� ������������ � �� ������
    endpoints.MapControllers();
  });
}