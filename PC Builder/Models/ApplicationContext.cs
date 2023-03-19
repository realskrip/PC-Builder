using Microsoft.EntityFrameworkCore;

namespace PC_Builder.Models
{
    public class ApplicationContext : DbContext
    {
        // DbSet коллекции объектов, которые сопоставляются с определенной таблицей в базе данных.
        public DbSet<CPU> CPUs { get; set; } = null!;
        public DbSet<Cooling> Coolings { get; set; } = null!;
        public DbSet<Motherboard> Motherboards { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}