namespace CityInfo.API.Models
{
    public class CityInfoUserDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public CityInfoUserDto(int userID, string username, string firstName, string lastName, string city)
        {
            UserID = userID;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}
