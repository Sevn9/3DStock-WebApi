using Microsoft.EntityFrameworkCore;
using Stock3D.Domain;
using System;

namespace Stock3D.Application.Interfaces
{
  public interface IStock3DDbContext
  {
    DbSet<Model3D> Models3D { get; set; }
    //сохраняет изменение контекста в базу данных
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
