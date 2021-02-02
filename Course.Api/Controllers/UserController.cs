using BC = BCrypt.Net.BCrypt;
using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using Course.Api.Configurations;
using Course.Api.Filters;
using Course.Api.Models;
using Course.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Course.Api.Controllers
{
    [Route("api/v1/user")] // Rota para esse controller (api/userController)
    [ApiController] // Assina ou anota como apiController
    public class UserController : ControllerBase //Herda da classe ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController(
            IUserRepository userRepository,
             IAuthenticationService authenticationService
            )
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// This service authenticates a registered user
        /// </summary>
        /// <param name="loginViewModelInput">View model of login</param>
        /// <returns>Returns status ok, data of the user and the token in access case </returns>
        [SwaggerResponse(statusCode: 200, description: "Success in authenticating", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required fields", Type = typeof(ValidateFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(ErrorGenericViewModel))]
        [HttpPost] //Informo que essa rota vai ter o verbo post do http
        [Route("login")]
        [ValidationModelStateCustom] // filtro genérico, que captura erros
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            var user = _userRepository.GetUser(loginViewModelInput.Login);

            if (!_userRepository.Authenticate(user, loginViewModelInput))
            {
                return BadRequest("There was an error when trying to access");
            }

            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = user.Code,
                Login = loginViewModelInput.Login,
                Email = user.Email
            };

            string token = _authenticationService.GetToken(userViewModelOutput);

            return Ok(new
            {
                Token = token,
                User = userViewModelOutput
            });
        }

        /// <summary>
        /// This service allows you to register a user
        /// </summary>
        /// <param name="registerViewModelInput">View model of login register</param>
        /// <returns>Returns status create and input model </returns>
        [SwaggerResponse(statusCode: 201, description: "Success in creating", Type = typeof(RegisterViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required fields", Type = typeof(ValidateFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(ErrorGenericViewModel))]
        [HttpPost] //Informo que essa rota vai ter o verbo post do http
        [Route("register")]
        [ValidationModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            var user = new UserEntity();

            user.Login = registerViewModelInput.Login;
            user.Password = BC.HashPassword(registerViewModelInput.Password);
            user.Email = registerViewModelInput.Email;

            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", registerViewModelInput); // Retorna status 201 (created)
        }
    }
}
