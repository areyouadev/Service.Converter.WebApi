namespace Service.Converter.WebApi.Domain.Commands.Users
{
    using System;
    
    using Contracts;

    public class UpdateCommand : ICommand
    {
        public UpdateCommand() { }
        public UpdateCommand(Guid id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
