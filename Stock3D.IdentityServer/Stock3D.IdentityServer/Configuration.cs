using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.IdentityServer
{
  public class Configuration
  {
    //scope или область представляет то что клиентскому приложению можно использовать
    public static IEnumerable<ApiScope> ApiScopes =>
      new List<ApiScope>
      {
        new ApiScope("Stock3DWebAPI", "Web API")
      };
    //аутенфикатор отправляется в процессе аутенфикации результате запроса токена
    // т.е. доступ на уровне области, в айдентите сервере области представлены ресурсами
    // это могут быть айдентити и айпиай ресурсы

    //айдентити ресурс позволяет смоделировать область которая позволит клиентскому приложению
    //просматривать подмножество утверждений о пользователе, например область профайл позволяет
    //видеть приложению утверждения о пользователе такие как имя или дата рождения
    //утверждения еще называются клеймами
    public static IEnumerable<IdentityResource> IdentityResources =>
      new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()

      };
    // апиай ресурс позволяет смоделировать доступ ко всему защищенному ресурсу
    // и айпиай с отдельными уровнями разрешений или областями которым
    // клиентское приложение может запросить доступ в нашем случае назовем ресурс нотевебапи
    // и scopesnoteapi и массив клейиор или утверждение передадим claim name
    public static IEnumerable<ApiResource> ApiResources =>
      new List<ApiResource>
      {
        new ApiResource("Stock3DWebAPI", "Web API", new []
        {JwtClaimTypes.Name})
        {
          Scopes = {"Stock3DWebAPI"}
        }
      };
    // айдентити серверу необходимо знать каким клиентским приложениям позволено использовать его
    // можно думать как о списке приложений которые могут использовать нашу систему 
    // каждое клиентское приложение конфигурируется так что ему позволено делать
    // только определенный набор вещей (client id это идентификатор клиента в
    // конфигурации клиента должен быть точно такой же (client id должен быть таким же и на клиенте и на сервере))

    // гранд типы протоко flow? grant types определяет как клиент взаимодействует с сервисом токеном
    public static IEnumerable<Client> Clients =>
      new List<Client>
      {
        new Client
        {
          ClientId = "stock3d-web-api",
          ClientName = "Stock3D Web",
          AllowedGrantTypes = GrantTypes.Code,
          RequireClientSecret = false,
          RequirePkce = true,
          //перенаправление после аутенфикации клиентского приложения
          RedirectUris =
          {
            "http://localhost:3000/signin-oidc"
          },
          AllowedCorsOrigins =
          {
            "http://localhost:3000"
          },
          PostLogoutRedirectUris =
          {
            "http://localhost:3000/signout-oidc"
          },
          //области доступные клиенту
          AllowedScopes =
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "Stock3DWebAPI"
          },
          //управляет токеном доступа через браузер, может предотвратить утечку токена доступа
          AllowAccessTokensViaBrowser= false,
        }
      };
  }
}
