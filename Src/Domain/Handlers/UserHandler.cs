using System;

namespace Service.Converter.WebApi.Domain.Handlers
{
    using Commands;
    using Entities;
    using Security;
    using Contracts;
    using Repositories;
    using Commands.Users;
    using Service.Converter.WebApi.Domain.Commands.Contracts;

    public class UserHandler :
     IHandler<CreateCommand>,
     IHandler<UpdateCommand>,
     IHandler<UpdatePassawordCommand>,
     IHandler<DeleteCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(CreateCommand command)
        {
             var user = new User(command.Username, command.Email, HashingBCrypt.HashPassword(command.Password));
            
            if (!user.IsValid)
                return new GenericCommandResult(false, "Invalid data", user.ValidationResult.Errors);

            if( _userRepository.GetByEmail(user.Email) == null)
                _userRepository.Add(user);
            else
                return new GenericCommandResult(false, "User with same email already exists", user.ValidationResult.Errors);

            return new GenericCommandResult(true, "User registered successfully", user);
        }
        public ICommandResult Handle(UpdateCommand command)
        {
            var user = _userRepository.GetById(command.Id);
            user.Update(command.Username, command.Email);
            if (!user.IsValid)
                return new GenericCommandResult(false, "Invalid data", user.ValidationResult.Errors);

            _userRepository.Update(user);

            return new GenericCommandResult(true, "User updated successfully", user);
        }
        public ICommandResult Handle(UpdatePassawordCommand command)
        {
            if (command.Password != command.ConfirmPassword)
                return new GenericCommandResult(false, "Password and Confirm Password is not matching", null);

            var user = _userRepository.GetById(command.Id);

            user.UpdatePassword(HashingBCrypt.HashPassword(command.Password));
            if (!user.IsValid)
                return new GenericCommandResult(false, "Invalid data", user.ValidationResult.Errors);

            _userRepository.Update(user);

            return new GenericCommandResult(true, "User password updated successfully", null);
        }

        public ICommandResult Handle(DeleteCommand command)
        {
            var user = _userRepository.GetById(command.Id);

            if (user == null)
                return new GenericCommandResult(false, "User not found", null);

            _userRepository.Delete(user);

            return new GenericCommandResult(true, "User deleted successfully", null);
        }
    }
}
