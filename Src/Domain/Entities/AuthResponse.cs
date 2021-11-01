namespace Service.Converter.WebApi.Domain.Entities
{
    public class AuthResponse
    {
        public AuthResponse(User user, string token)
        {
            User = user;
            Token = token;
        }

        public User User { get; set; }

        public string  Token { get; set; }
    }
}
