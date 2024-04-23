using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool isIncludePointsOfInterest = false)
        {
            //var cityToReturn = citiesDataStore.Cities.FirstOrDefault(c => c.ID == id);

            //if (cityToReturn == null) 
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);

            var city = await _cityInfoRepository.GetCityByIDAsync(id, isIncludePointsOfInterest);
            if (city == null) 
            {
                return NotFound();
            }

            if (isIncludePointsOfInterest) 
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));

        }

    }
}
