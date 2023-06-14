using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stock3D.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.IdentityServer.Data
{
  public class AuthDbContext : IdentityDbContext<AppUser>
  {
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    //переопределим метод OnModelCreating
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // сделаем маппинг сущностей
      builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
      builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
      builder.Entity<IdentityUserRole<string>>(entity =>
      entity.ToTable(name: "UserRoles"));
      builder.Entity<IdentityUserClaim<string>>(entity =>
      entity.ToTable(name: "UserClaim"));
      builder.Entity<IdentityUserLogin<string>>(entity =>
                entity.ToTable("UserLogins"));
      builder.Entity<IdentityUserToken<string>>(entity =>
          entity.ToTable("UserTokens"));
      builder.Entity<IdentityRoleClaim<string>>(entity =>
          entity.ToTable("RoleClaims"));

      builder.ApplyConfiguration(new AppUserConfiguration());
    }
  }
}
