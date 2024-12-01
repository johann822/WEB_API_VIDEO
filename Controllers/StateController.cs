using Microsoft.AspNetCore.Mvc;
using WebAPITest.DAL.Entities;
using WebAPITest.Domain.Interfaces;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]//Nombre inicial de mi ruta,URL o PATH
    [ApiController]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }
        [HttpGet, ActionName("Get")]
        [Route("GetByCountryId")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesByIdCountryAsync(Guid id)
        {
            var states = await _stateService.GetStatesByIdCountryAsync(id);

            if (states == null || !states.Any()) return NotFound();

            return Ok(states);
        }
        [HttpGet, ActionName("Get")]
        [Route("GetByName")]
        public async Task<ActionResult<IEnumerable<State>>> GetStateByNameAsync(String name)
        {
            var states = await _stateService.GetStateByNameAsync(name);

            if (states == null) return NotFound();//Not Found = status code 404

            return Ok(states);//Ok = status code 200
        }
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<State>> CreateStateAsync(State state)
        {
            try
            {
                var new_states = await _stateService.CreateStateAsync(state);

                if (new_states == null) return NotFound();

                return Ok(new_states);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));
                return Conflict(ex.Message);
            }
        }
        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state)
        {
            try
            {
                var editedstate = await _stateService.EditStateAsync(state);

                if (editedstate == null) return NotFound();

                return Ok(editedstate);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(String name)
        {
            if (name == null) return BadRequest();

            var Deletedstate = await _stateService.DeleteStateAsync(name);

            if (Deletedstate == null) return NotFound();

            return Ok(Deletedstate);
        }
    }
}
