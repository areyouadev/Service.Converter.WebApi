namespace Service.Converter.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Security;
    
    using Domain.Entities;
    using Domain.Commands;
    using Domain.Repositories;

    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult<AuthResponse> Authenticate([FromBody] AuthCommand command)
        {
            var user = _userRepository.GetByEmail(command.Email);

            if (user == null)
                return NotFound(new { message = "UserName or Password is invalid" });

            if (!HashingBCrypt.ValidatePassword(command.Password, user.Password))
                return NotFound(new { message = "UserName or Password is invalid" });

            var token = TokenService.GenerateToken(user);

            user.HidePassword();

            var authResponse = new AuthResponse(user, token);

            return Ok(authResponse);
        }
    }
}
