using Microsoft.Extensions.Options;
using Stock3D.Application;
using Stock3D.Application.Common.Mappings;
using Stock3D.Application.Interfaces;
using Stock3D.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();
//var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//Configure(app, provider);
Configure(app);

app.MapGet("/test", () => "Hello World!");

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
  /*
  services.AddAuthentication(config =>
  {
    //����� �� ��������� ��� ������������
    //config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
    .AddJwtBearer("Bearer", option =>
    {
      option.Authority = "https://localhost:44358/";
      option.Audience = "NotesWebAPI";
      option.RequireHttpsMetadata = false;
    });

  services.AddVersionedApiExplorer(options =>
  options.GroupNameFormat = "'v'VVV");
  services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
    ConfigureSwaggerOptions>();

  */
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

//void Configure(WebApplication app, IApiVersionDescriptionProvider provider)
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
  /*
  
  app.UseSwaggerUI(config =>
  {
    foreach (var description in provider.ApiVersionDescriptions)
    {
      config.SwaggerEndpoint(
        $"/swagger/{description.GroupName}/swagger.json",
        description.GroupName.ToUpperInvariant());
      config.RoutePrefix = string.Empty;

    }

    //config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
  });
  //����������� ��������� �������� � ��������
  app.UseCustomExceptionHandler();
  */
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
  /*
  //������� ����������� � ������������
  //app.UseAuthentication();
  //app.UseAuthorization();
  //app.UseApiVersioning();
  */

  app.UseEndpoints(endpoints =>
  {
    //������� ����� �������� �� �������� ������������ � �� ������
    endpoints.MapControllers();
  });
}