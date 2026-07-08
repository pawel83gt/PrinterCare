using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Entities;

namespace PrinterCare.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Branch> Branches { get; set; }

        //конструктор принимает параметр options — это объект, который содержит все настройки контекста
        //(например, строку подключения, провайдер базы данных, логирование и т.д.).
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
