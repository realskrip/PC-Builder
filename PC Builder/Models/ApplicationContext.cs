using Microsoft.EntityFrameworkCore;

namespace PC_Builder.Models
{
    public class ApplicationContext : DbContext
    {
        // DbSet коллекции объектов, которые сопоставляются с определенной таблицей в базе данных.
        public DbSet<CPU_Manufacturer> CPU_Manufacturers { get; set; } = null!;
        public DbSet<CPU> CPUs { get; set; } = null!;
        public DbSet<CoolingType> CoolingTypes { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}