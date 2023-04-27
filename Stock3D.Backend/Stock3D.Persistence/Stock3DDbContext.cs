using Microsoft.EntityFrameworkCore;
using Stock3D.Application.Interfaces;
using Stock3D.Domain;
using Stock3D.Persistence.EntityTypeConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Persistence
{
  public class Stock3DDbContext : DbContext, IStock3DDbContext
  {
    public DbSet<Model3D> Models3D { get; set; }

    public Stock3DDbContext(DbContextOptions<Stock3DDbContext> options) :
      base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new Model3DConfiguration());
      base.OnModelCreating(modelBuilder);
    }

  }
}
