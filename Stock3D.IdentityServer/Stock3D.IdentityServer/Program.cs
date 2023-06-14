using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Stock3D.IdentityServer;
using Stock3D.IdentityServer.Data;
using Stock3D.IdentityServer.Models;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DbConnection");
RegisterServices(builder.Services);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
Configure(app);
app.Run();
void RegisterServices(IServiceCollection services)
{
  services.AddDbContext<AuthDbContext>(options =>
  {
    options.UseNpgsql(connectionString);
  });
  //��������� ��������� � ������������� ��� ��� ������� � ��������� ���
  services.AddIdentity<AppUser, IdentityRole>(config =>
  {
    //���������� � ������
    config.Password.RequiredLength = 4;
    //���������� � ������������� ��������� ����
    config.Password.RequireDigit = false;
    //���������� � ������������� ����
    config.Password.RequireNonAlphanumeric = false;
    //���������� � ������������� ����������-�������� ��������
    config.Password.RequireUppercase = false;

  })
    .AddEntityFrameworkStores<AuthDbContext>() //������� ���������� � ��������� Identity
    .AddDefaultTokenProviders(); //��������� ����� ���������� ��� ��������� � ���������� ������� �������

  //������������ IdentityServer4 � ����� ���������� ���������
  //������������ � ������� AddIdentityServer
  //AddDeveloperSigningCredential - ���������������� ���������� �������
  // ��������� AddIdentityServer �� ���������� ��� ��������� ����� ��������� � ������
  
  services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();
  
  // ������������� Cookie ��� �������� �������� �������� ������
  services.ConfigureApplicationCookie(config =>
  {
    config.Cookie.Name = "Stock3D.IdentityServer.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
  });

  services.AddControllersWithViews();
}

void Configure(WebApplication app)
{
  //����� ��� ������ ��������� ������� �� ����
  using var scope = app.Services.CreateScope();
  var serviceProvider = scope.ServiceProvider;
  
  try
  {
    var context = serviceProvider.GetRequiredService<AuthDbContext>();
    DbInitializer.Initialize(context);
  }
  catch (Exception exception)
  {
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, "An error occured while app initialization");
  }
  
  if (app.Environment.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }
  //������� ������������� ����������� ������
  app.UseStaticFiles(new StaticFileOptions
  {
    FileProvider = new PhysicalFileProvider(
      Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/Styles"
  });
  app.UseRouting();
  //middleware �������� ������ ������������� ��� �������� ����� oauth � oidc
  app.UseIdentityServer();
  app.UseHttpsRedirection();

  app.UseEndpoints(endpoints =>
  {
    endpoints.MapDefaultControllerRoute();
  });

}