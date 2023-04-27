using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock3D.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Persistence.EntityTypeConfiguration
{
  public class Model3DConfiguration: IEntityTypeConfiguration<Model3D>
  {
    public void Configure(EntityTypeBuilder<Model3D> builder)
    {
      //Параметры:
      //id это ключ, он уникален
      builder.HasKey(model3D => model3D.Id);
      builder.HasIndex(model3D => model3D.Id).IsUnique();
      builder.Property(model3D => model3D.Title).HasMaxLength(250);

    }


  }
}
