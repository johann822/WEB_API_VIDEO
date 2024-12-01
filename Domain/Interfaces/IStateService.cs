using WebAPITest.DAL.Entities;

namespace WebAPITest.Domain.Interfaces
{
    public interface IStateService
    {
        Task<State> GetStateByNameAsync(String name);

        Task<State> CreateStateAsync(State state);

        Task<IEnumerable<State>> GetStatesByIdCountryAsync(Guid id);

        Task<State> EditStateAsync(State state);

        Task<State> DeleteStateAsync(String name);
    }
}
