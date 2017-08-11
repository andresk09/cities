using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpLab.API.Application;
using System.Net;
using OpLab.API.Application.Dtos;

namespace OpLab.API.Controllers
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }
        
        [HttpGet]
        public async Task<List<CityDto>> GetAsync()
        {
            return await _cityService.Get();                                   
        }
        
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var city = await _cityService.Get(new GetCityInput { Id = id });

                if (city != null && city.Cities.Any())
                    return Ok(city.Cities.First());

                return NotFound();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CityDto city)
        {
            if (city == null)
            {
                return BadRequest();
            }

            if (city.Description == city.Name)
            {
                ModelState.AddModelError(nameof(city.Description), "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _cityService.Create(city);
            
                return CreatedAtRoute("Get", new { id = city.Id }, city);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CityDto cityToUpdate)
        {
            if (cityToUpdate == null)
            {
                return BadRequest();
            }

            if (cityToUpdate.Description == cityToUpdate.Name)
            {
                ModelState.AddModelError(nameof(cityToUpdate.Description), "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var cities = await _cityService.Get(new GetCityInput { Id = id });

                if (cities == null || !cities.Cities.Any() || cities.Cities.First() == null)
                {
                    return NotFound();                
                }

                var city = cities.Cities.First();
                city.Name = cityToUpdate.Name;
                city.Description = cityToUpdate.Description;

                await _cityService.Update(city);

                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var cities = await _cityService.Get(new GetCityInput { Id = id });

                if (cities == null || !cities.Cities.Any() || cities.Cities.First() == null)
                {
                    return NotFound();
                }

                var city = cities.Cities.First();

                await _cityService.Remove(city);

                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
