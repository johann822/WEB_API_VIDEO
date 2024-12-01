using Microsoft.EntityFrameworkCore;
using WebAPITest.DAL.Entities;
using WebAPITest.DAL;
using WebAPITest.Domain.Interfaces;

namespace WebAPITest.Domain.Services
{
    public class StateService : IStateService
    {
        private readonly DataBaseContext _context;
        public StateService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<State> CreateStateAsync(State state)
        {
            try
            {
                state.Id = Guid.NewGuid();
                state.CreatedDate = DateTime.Now;
                _context.States.Add(state); //Metodo add permite crea objeto en el contexto de base de datos
                await _context.SaveChangesAsync(); //Guardar pais en tabla contry
                return state;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }
        public async Task<IEnumerable<State>> GetStatesByIdCountryAsync(Guid countryid)
        {
            try
            {
                return _context.States.Where(s=>s.CountryId == countryid);
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }

        public async Task<State> EditStateAsync(State state)
        {
            try
            {
                state.ModifiedDate = DateTime.Now;
                _context.States.Update(state);
                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }
        public async Task<State> DeleteStateAsync(String name)
        {
            try
            {
                var state = await GetStateByNameAsync(name);
                if (state != null)
                {
                    return state;
                }
                _context.States.Remove(state);
                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }
        }

        public async Task<State> GetStateByNameAsync(String name)
        {
            try
            {
                return await _context.States.FirstOrDefaultAsync(c => c.Name == name);
            }
            catch (DbUpdateException dbUdateException)
            {
                throw new Exception(dbUdateException.InnerException?.Message ?? dbUdateException.Message);
            }


        }

    }
}
