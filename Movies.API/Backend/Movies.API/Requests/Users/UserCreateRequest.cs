namespace Movies.API.Requests.Users
{
    public class UserCreateRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}