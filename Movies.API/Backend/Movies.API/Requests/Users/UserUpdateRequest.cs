namespace Movies.API.Requests.Users
{
    public class UserUpdateRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
