using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.IdentityServer.Data
{
  public class DbInitializer
  {
    public static void Initialize(AuthDbContext context)
    {
      context.Database.EnsureCreated();  // создаем базу данных при первом обращении
    }
  }
}
