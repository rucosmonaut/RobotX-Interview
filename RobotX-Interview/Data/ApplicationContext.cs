using Microsoft.EntityFrameworkCore;
using RobotX_Interview.Entities;

namespace RobotX_Interview.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new { Id = 1,
                     CardCode = 6999002655657, 
                     StartDate = DateTime.Now,
                     FinishDate = DateTime.Now, 
                     LastName = "Иванов",
                     FirstName = "Виталий",
                     SurName = "Петрович",
                     Gender = "М",
                     BirthDay = DateTime.Now,
                     PhoneHome = "",
                     PhoneMobil = "+7999-554-44-66",
                     Email = "vv@robotx.ru",
                     City = "kaliningrad",
                     Street = "Московский пр-кт",
                     House = "40",
                     Apartment = "2"
                });
        }
    }
}
