namespace Service.Converter.WebApi.Domain.Commands.Users
{
    using System;

    using Contracts;

    public class UpdatePassawordCommand : ICommand
    {
        public UpdatePassawordCommand() { }
        public UpdatePassawordCommand(Guid id, string password, string confirmPassword)
        {
            Id = id;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public Guid Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
