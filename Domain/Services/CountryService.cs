using Microsoft.EntityFrameworkCore;
using WebAPITest.DAL;
using WebAPITest.DAL.Entities;
using WebAPITest.Domain.Interfaces;

namespace WebAPITest.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;
        public CountryService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
            
        }
        public async Task<Country> CreateCountryAsync(Country country)
        {
            

            try
            {
                country.Id = Guid.NewGuid();
                country.CreatedDate = DateTime.Now;
                _context.Countries.Add(country); //Metodo add permite crea objeto en el contexto de base de datos
                await _context.SaveChangesAsync(); //Guardar pais en tabla contry
                return country;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message?? dbUdateException.Message);
            }
        }
        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            try
            {
                return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
            
            
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }
        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                var country = await GetCountryByIdAsync(id);
                if (country != null)
                {
                    return country;
                }
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }

    }
}
