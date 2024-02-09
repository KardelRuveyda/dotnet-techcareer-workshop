using DotnetWorkshop.Core.Models;
using DotnetWorkshop.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotnetWorkshop.Repository
{
    public class AppDbContext:DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aşağıdaki gibi de uygulayabilirsiniz ama pek önerilen bir kod değildir.
            //modelBuilder.ApplyConfiguration(new TeamConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new UserProfileConfiguration());

            //Assembly içinde bulunan tüm Fluent API Konfigürasyon sınıflarını çalıştır. 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
