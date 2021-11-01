namespace Service.Converter.WebApi.Domain.Commands.Users
{
    using System;

    using Contracts;

    public class DeleteCommand : ICommand
    {
        public DeleteCommand() { }
        public DeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

}
