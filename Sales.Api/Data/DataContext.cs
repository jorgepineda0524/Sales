using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(o => o.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("CountryId","Name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("StateId","Name").IsUnique();
        }
    }
}
