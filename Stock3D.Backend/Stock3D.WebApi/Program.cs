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

//app.MapGet("/", () => "Hello World!");

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
  

}
//��������� ���������(���������) ��������� ��� ����� ������������ ���������� �����
//���� �� ������������ ��������

void Configure(WebApplication app)
{
  if (app.Environment.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }

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