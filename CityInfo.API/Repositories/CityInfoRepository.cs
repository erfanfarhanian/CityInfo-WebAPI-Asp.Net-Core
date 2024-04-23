using CityInfo.API.DBContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDBContext _context;

        public CityInfoRepository(CityInfoDBContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task AddPointOfInterestForCityAsync(int cityID, PointOfInterest pointOfInterest)
        {
            var city = await GetCityByIDAsync(cityID, false);

            if (city != null)
            {
                city.PointOfInterest.Add(pointOfInterest);
            }
        }

        public void DeletePointOfInterestAsync(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityByIDAsync(int cityID, bool isIncludePointsOfInterest)
        {
            if (isIncludePointsOfInterest) 
            {
                return await _context.Cities
                    .Where(c => c.ID == cityID)
                    .Include(c => c.PointOfInterest)
                    .FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.ID == cityID).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityByIDAsync(int cityID, int pointOfInterestID)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityID == cityID && p.ID == pointOfInterestID)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityID)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityID == cityID)
                .ToListAsync();
        }

        public async Task<bool> IsCityExistAsync(int cityID)
        {
            return await _context.Cities.AnyAsync(c => c.ID == cityID);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
