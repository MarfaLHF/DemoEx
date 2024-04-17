using DemoEx.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoEx.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; } // Добавляем сущность User в контекст
        public DbSet<TypeEquipment> TypeEquipments { get; set; } // Добавляем сущность User в контекст
        public DbSet<TypeProblem> TypeProblem { get; set; } // Добавляем сущность User в контекст
        public DbSet<User> Users { get; set; } // Добавляем сущность User в контекст


    }
}
