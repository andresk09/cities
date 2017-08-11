using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OpLab.API.Application.Dtos;
using OpLab.API.Application;

namespace OpLab.API.Controllers
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private readonly IPointOfInterestService _pointOfInterestService;

        private readonly ICityService _cityService;

        public PointsOfInterestController(IPointOfInterestService pointOfInterestService, ICityService cityService)
        {
            _pointOfInterestService = pointOfInterestService;
            _cityService = cityService;
        }
        
        [HttpGet("{cityId}/pointsofinterest")]
        public async Task<IActionResult> GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!_cityService.CityExists(cityId).Result)
                {
                    return NotFound();
                }

                var pointsOfInterestForCity = await _pointOfInterestService.GetForCity(cityId);

                return Ok(pointsOfInterestForCity);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}")]
        public async Task<IActionResult> GetAsync(int cityId, int id)
        {
            try
            {
                if (!_cityService.CityExists(cityId).Result)
                {
                    return NotFound();
                }

                var pointsOfInterest = await _pointOfInterestService.Get(cityId, id);

                if (pointsOfInterest != null)
                    return Ok(pointsOfInterest);

                return NotFound();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public async Task<IActionResult> PostAsync(int cityId, [FromBody] PointOfInterestDto pointOfInterest)
        {
            if (!_cityService.CityExists(cityId).Result)
            {
                return NotFound();
            }

            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(nameof(pointOfInterest.Description), "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                pointOfInterest.CityId = cityId;
                await _pointOfInterestService.Create(pointOfInterest);

                return CreatedAtRoute("Get", new { id = pointOfInterest.Id }, pointOfInterest);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public async Task<IActionResult> PutAsync(int cityId, int id, [FromBody] PointOfInterestDto UpdatedPointOfInterest)
        {
            if (UpdatedPointOfInterest == null)
            {
                return BadRequest();
            }

            if (UpdatedPointOfInterest.Description == UpdatedPointOfInterest.Name)
            {
                ModelState.AddModelError(nameof(UpdatedPointOfInterest.Description), "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!_cityService.CityExists(cityId).Result)
                {
                    return NotFound();
                }

                var pointOfInterest = await _pointOfInterestService.Get(cityId, id);

                if (pointOfInterest == null)
                {
                    return NotFound();
                }

                pointOfInterest.Name = UpdatedPointOfInterest.Name;
                pointOfInterest.Description = UpdatedPointOfInterest.Description;

                await _pointOfInterestService.Update(pointOfInterest);

                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public async Task<IActionResult> DeleteAsync(int cityId, int id)
        {
            try
            {
                if (!_cityService.CityExists(cityId).Result)
                {
                    return NotFound();
                }

                var pointsOfInterest = await _pointOfInterestService.Get(cityId, id);

                if (pointsOfInterest == null)
                {
                    return NotFound();
                }                

                await _pointOfInterestService.Remove(pointsOfInterest);

                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
