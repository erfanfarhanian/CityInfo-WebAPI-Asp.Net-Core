using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities {  get; set; }

        // public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    ID = 1,
                    Name = "Tehran",
                    Description = "This is Tehran City.",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            ID = 1,
                            Name = "Point Of 1",
                            Description = "This is the Point Of 1"
                        },
                        new PointOfInterestDto()
                        {
                            ID = 2,
                            Name = "Point Of 2",
                            Description = "This is the Point Of 2"
                        }
                    }
                },
                new CityDto()
                {
                    ID = 2,
                    Name = "Shiraz",
                    Description = "This is Shiraz City.",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            ID = 3,
                            Name = "Point Of 3",
                            Description = "This is the Point Of 3"
                        },
                        new PointOfInterestDto()
                        {
                            ID = 4,
                            Name = "Point Of 4",
                            Description = "This is the Point Of 4"
                        }
                    }
                },
                new CityDto()
                {
                    ID = 3,
                    Name = "Ahwaz",
                    Description = "This is Ahwaz City.",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            ID = 5,
                            Name = "Point Of 5",
                            Description = "This is the Point Of 5"
                        },
                        new PointOfInterestDto()
                        {
                            ID = 6,
                            Name = "Point Of 6",
                            Description = "This is the Point Of 6"
                        }
                    }
                }
            };
        }
    }
}
