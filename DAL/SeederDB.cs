using WebAPITest.DAL.Entities;

namespace WebAPITest.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;

        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }

        //Seeder asign 
        //Metodo MAIN()
        //Este metodo prepobla diff tablas de la BD

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            //Metodos para repoblar bd

            await populateCountriesAsync();

            await _context.SaveChangesAsync();
        }
        #region Private methods
        private async Task populateCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Antioquia"
                        },
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Cundinamarca"
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Buenos Aires"
                        },
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "La Pampa"
                        }
                    }
                });
            }
        }
        #endregion
    }
}
