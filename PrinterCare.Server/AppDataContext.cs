using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Models;

namespace PrinterCare.Server
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //конструктор принимает параметр options — это объект, который содержит все настройки контекста
        //(например, строку подключения, провайдер базы данных, логирование и т.д.).
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
