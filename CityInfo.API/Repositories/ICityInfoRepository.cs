using CityInfo.API.Entities;

namespace CityInfo.API.Repositories
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIDAsync(int cityID, bool isIncludePointsOfInterest);
        Task<bool> IsCityExistAsync(int cityID);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityID);
        Task<PointOfInterest?> GetPointOfInterestForCityByIDAsync(int cityID, int pointOfInterestID);
        Task AddPointOfInterestForCityAsync(int cityID, PointOfInterest pointOfInterest);
        void DeletePointOfInterestAsync(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
