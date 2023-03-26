using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RentACarDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        DbSet<Car> Cars { get; set; }
        DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6RR1DUB\\KELERMAN; Database=RentACarDataBase; Integrated Security=true");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Request>()
                .HasOne(uc => uc.User)
               .WithMany(r => r.Requests)
               .HasForeignKey(r => r.UserId);

            builder.Entity<Request>()
                .HasOne(c => c.Car)
                .WithMany(r => r.Requests)
                .HasForeignKey(r => r.CarId);

            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<User>().HasIndex(u => u.EGN).IsUnique();
            base.OnModelCreating(builder);
        }
    }
}