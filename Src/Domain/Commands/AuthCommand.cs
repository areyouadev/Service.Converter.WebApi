namespace Service.Converter.WebApi.Domain.Commands
{
    using Contracts;

    public class AuthCommand : ICommand
    {
        public AuthCommand() { }
        public AuthCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
