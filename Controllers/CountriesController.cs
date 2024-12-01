using Microsoft.AspNetCore.Mvc;
using WebAPITest.DAL.Entities;
using WebAPITest.Domain.Interfaces;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]//Nombre inicial de mi ruta,URL o PATH
    [ApiController]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync();

            if (countries == null || !countries.Any()) return NotFound();

            return Ok(countries);
        }
        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]//Api/Countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesByIdAsync(Guid id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);

            if (country == null) return NotFound();//Not Found = status code 404

            return Ok(country);//Ok = status code 200
        }
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<Country>> CreateCountryAsync(Country country) 
        {
            try
            {
                var new_conuntry =await _countryService.CreateCountryAsync(country);

                if (new_conuntry == null) return NotFound();

                return Ok(new_conuntry);
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
                return Conflict(ex.Message);
            }
        }
        [HttpPut,ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Country>> EditCountryAsync(Country country) 
        {
            try
            {
                var editedcountry =  await _countryService.EditCountryAsync(country);

                if (editedcountry == null) return NotFound(); 

                return Ok(editedcountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Country>> DeleteCountryAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var Deletedcountry = await _countryService.DeleteCountryAsync(id);

            if (Deletedcountry == null) return NotFound();

            return Ok(Deletedcountry);
        }
    }
}
