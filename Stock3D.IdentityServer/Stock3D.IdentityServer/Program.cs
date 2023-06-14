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
  //добавл€ем айдентити и конфигурируем его дл€ аппюзер и айдентити рол
  services.AddIdentity<AppUser, IdentityRole>(config =>
  {
    //требовани€ к паролю
    config.Password.RequiredLength = 4;
    //требование к использованию заглавных букв
    config.Password.RequireDigit = false;
    //требование к использованию цифр
    config.Password.RequireNonAlphanumeric = false;
    //требование к использованию небуквенно-цифровых символов
    config.Password.RequireUppercase = false;

  })
    .AddEntityFrameworkStores<AuthDbContext>() //добавим дбконтекст в хранилище Identity
    .AddDefaultTokenProviders(); //дефолтные токен провайдеры дл€ получени€ и обновлени€ токенов доступа

  //регистрируем IdentityServer4 в своем контейнере внедрени€
  //зависимостей с помощью AddIdentityServer
  //AddDeveloperSigningCredential - демонстрационный сертификат подписи
  // использу€ AddIdentityServer мы заставл€ем все временные файлы хранитьс€ в пам€ти
  
  services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();
  
  // использование Cookie дл€ хранени€ текущего значени€ токена
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
  //чтобы при старте провер€ть создана ли база
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
  //добавим использование статических файлов
  app.UseStaticFiles(new StaticFileOptions
  {
    FileProvider = new PhysicalFileProvider(
      Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/Styles"
  });
  app.UseRouting();
  //middleware позволит начать маршрутизацию дл€ конечных точек oauth и oidc
  app.UseIdentityServer();
  app.UseHttpsRedirection();

  app.UseEndpoints(endpoints =>
  {
    endpoints.MapDefaultControllerRoute();
  });

}