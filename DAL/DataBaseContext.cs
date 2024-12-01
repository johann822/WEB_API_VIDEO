using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebAPITest.DAL.Entities;

namespace WebAPITest.DAL
{
    public class DataBaseContext : DbContext
    {
        // Me conecto a la BD por medio del constructor 
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        // Este metodo me sirve para configurar unos indices de cada campo de una BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); // Aqui creo un indice del campo Name para la tabla Countries
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();
        }

        #region Dbsets
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States{ get; set; }

        #endregion
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WebAPI_Jueves_2024II;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
