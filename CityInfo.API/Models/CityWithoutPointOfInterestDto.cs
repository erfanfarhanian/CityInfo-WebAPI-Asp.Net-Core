﻿namespace CityInfo.API.Models
{
    public class CityWithoutPointOfInterestDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
