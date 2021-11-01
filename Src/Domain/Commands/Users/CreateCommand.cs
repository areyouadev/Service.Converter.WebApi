namespace Service.Converter.WebApi.Domain.Commands.Users
{
    using Contracts;

    public class CreateCommand : ICommand
    {
        public CreateCommand() { }
        public CreateCommand(string username, string email, string password, string confirmPassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
