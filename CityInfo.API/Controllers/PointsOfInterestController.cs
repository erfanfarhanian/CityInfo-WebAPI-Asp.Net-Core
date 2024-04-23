using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;
using System.Drawing;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityID}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        private readonly CitiesDataStore citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService localMailService,
            CitiesDataStore citiesDataStore,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            this.citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityID)
        {
            // Without databse
            //try
            //{
            //    // throw new Exception("Exception Sample...");
            //    var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);

            //    if (city == null)
            //    {
            //        _logger.LogInformation($"City With ID {cityID} Was Not Found.");
            //        return NotFound();
            //    }
            //    return Ok(city.PointOfInterest);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Exception Getting {cityID}", ex);
            //    return StatusCode(500, "A Problem Happend ....");
            //}

            if (!await _cityInfoRepository.IsCityExistAsync(cityID)) 
            {
                _logger.LogInformation($"{cityID} Not Found...");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityID);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointOfInterestID}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityID, int pointOfInterestID) 
        {
            // Without databse
            //var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);

            //if(city == null)
            //{
            //    return NotFound();
            //}

            //var point = city.PointOfInterest.FirstOrDefault(p => p.ID == pointOfInterestID);

            //if (point == null) 
            //{
            //    return NotFound();
            //}

            //return Ok(point);

            if (!await _cityInfoRepository.IsCityExistAsync(cityID))
            {
                _logger.LogInformation($"{cityID} Not Found...");
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityByIDAsync(cityID, pointOfInterestID);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        #region Post

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityID, PointOfInterestForCreationDto pointOfInterest)
        {
            // Without database
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var maxpointOfInterestID = citiesDataStore.Cities
            //    .SelectMany(c => c.PointOfInterest)
            //    .Max(p => p.ID);

            //var createPoint = new PointOfInterestDto()
            //{
            //    ID = ++maxpointOfInterestID,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //city.PointOfInterest.Add(createPoint);

            //return CreatedAtAction("GetPointOfInterest",
            //    new
            //    {
            //        cityID = cityID,
            //        pointOfInterestID = createPoint.ID
            //    },
            //    createPoint
            //    );

            if (!await _cityInfoRepository.IsCityExistAsync(cityID))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityID, finalPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterest = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityID = cityID,
                pointOfInterestID = createdPointOfInterest.ID
            }, createdPointOfInterest);
        }

        #endregion

        #region Edit

        [HttpPut("{pointOfInterestID}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityID, int pointOfInterestID, PointOfInterestForUpdateDto pointOfInterest)
        {
            // Without Database
            // Find City
            //var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// Find Point Of Interest
            //var point = city.PointOfInterest
            //    .FirstOrDefault(p => p.ID == pointOfInterestID);
            //if (point == null)
            //{
            //    return NotFound();
            //}

            //point.Name = pointOfInterest.Name;
            //point.Description = pointOfInterest.Description;

            //return NoContent();

            if (!await _cityInfoRepository.IsCityExistAsync(cityID))
            {
                return NotFound();
            }

            var resultPointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityByIDAsync(cityID, pointOfInterestID);

            if (resultPointOfInterest == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, resultPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Edit With Patch

        [HttpPatch("{pointOfInterestID}")]
        public async Task<ActionResult> PartiallyPointOfInterest(int cityID, int pointOfInterestID, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            //Without Database
            //// Find City
            //var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// Find Point Of Interest
            //var pointOfInterestFromStore = city.PointOfInterest
            //    .FirstOrDefault(p => p.ID == pointOfInterestID);
            //if (pointOfInterestFromStore == null)
            //{
            //    return NotFound();
            //}

            //var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            //{
            //    Name = pointOfInterestFromStore.Name,
            //    Description = pointOfInterestFromStore.Description,
            //};

            //patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //if (!TryValidateModel(pointOfInterestToPatch))
            //{
            //    return BadRequest();
            //}

            //pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            //pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            //return NoContent();

            if (!await _cityInfoRepository.IsCityExistAsync(cityID))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository
                .GetPointOfInterestForCityByIDAsync(cityID, pointOfInterestID);

            if (pointOfInterestEntity == null) 
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            await _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync(); 

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{pointOfInterestID}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityID, int pointOfInterestID)
        {
            // Without Database
            //// Find City
            //var city = citiesDataStore.Cities.FirstOrDefault(c => c.ID == cityID);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// Find Point Of Interest
            //var pointOfInterest = city.PointOfInterest
            //    .FirstOrDefault(p => p.ID == pointOfInterestID);
            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}

            //city.PointOfInterest.Remove(pointOfInterest);

            if (!await _cityInfoRepository.IsCityExistAsync(cityID))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityByIDAsync(cityID, pointOfInterestID);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterestAsync(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _localMailService.Send("Point Of Interest Deleted",
                $"Point Of Interest {pointOfInterestEntity.Name} With ID {pointOfInterestEntity.ID}");

            return NoContent();
        }

        #endregion
    }
}
