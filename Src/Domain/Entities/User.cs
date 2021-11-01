namespace Service.Converter.WebApi.Domain.Entities
{
    using System;
    using FluentValidation;

    public class User : Entity
    {
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;

            Validate(this, new UserValidator());
        }

        public string Username { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }

        public void Update(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }

        public void HidePassword()
        {
            Password = "";
        }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("The User Id is required");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("UserName is required")
                .MaximumLength(20).WithMessage("Username must have a maximum of 20 characters");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Email is required")
                .EmailAddress().WithMessage("Invalid Email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
