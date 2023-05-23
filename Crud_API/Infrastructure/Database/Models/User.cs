namespace Crud_API.Infrastructure.Database.Models
{
    public class UserResponse
    {
        public List<UserResult> Results { get; set; }
    }

    public class UserResult
    {
        public string Gender { get; set; }
        public UserName Name { get; set; }
        public UserLocation Location { get; set; }
        public string Email { get; set; }
        public UserLogin Login { get; set; }
        public UserDob Dob { get; set; }
        public UserRegistered Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public UserId Id { get; set; }
        public UserPicture Picture { get; set; }
        public string Nat { get; set; }
    }

    public class UserName
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class UserLocation
    {
        public UserStreet Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public UserCoordinates Coordinates { get; set; }
        public UserTimezone Timezone { get; set; }
    }

    public class UserStreet
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    public class UserCoordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class UserTimezone
    {
        public string Offset { get; set; }
        public string Description { get; set; }
    }

    public class UserLogin
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Md5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }
    }

    public class UserDob
    {
        public DateTime Date { get; set; }
        public int Age { get; set; }
    }

    public class UserRegistered
    {
        public DateTime Date { get; set; }
        public int Age { get; set; }
    }

    public class UserId
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class UserPicture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }
}
