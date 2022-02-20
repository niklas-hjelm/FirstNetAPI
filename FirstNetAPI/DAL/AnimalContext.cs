using FirstNetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstNetAPI.DAL
{
    public class AnimalContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VQ464FR;Initial Catalog=Demo Database=AnimalDB;Integrated Security=True;Pooling=False");
        }
    }

}
