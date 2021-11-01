namespace Service.Converter.WebApi.Domain.Handlers.Contracts
{
    using Service.Converter.WebApi.Domain.Commands.Contracts;

    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
