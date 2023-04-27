using System;

namespace Stock3D.Persistence
{
  public class DbInitializer
  {
    //запускается при старте приложения и проверяет создана ли бд и если нет, то создается на основе контекста
    public static void Initialize(Stock3DDbContext context)
    {
      context.Database.EnsureCreated();
    }

  }
}
