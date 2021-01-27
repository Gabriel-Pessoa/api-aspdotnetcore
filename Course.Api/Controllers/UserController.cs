﻿using Course.Api.Business.Entities;
using Course.Api.Filters;
using Course.Api.Infraestruture.Data;
using Course.Api.Models;
using Course.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Course.Api.Controllers
{
    [Route("api/v1/user")] // Rota para esse controller (api/userController)
    [ApiController] // Assina ou anota como apiController
    public class UserController : ControllerBase //Herda da classe ControllerBase
    {
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
            var userViewModelOutput = new UserViewModelOutput() 
            {
                Code = 1,
                Login = "Gabriel",
                Email = "gabriel@email.com"
            };

            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jh\\2.}8ZP'qY#7");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), // expiração do token de 1 dia
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new 
            {
                Token = token,
                User = userViewModelOutput
            }); // Retorna status 200 (tudo certo)
        }

        /// <summary>
        /// This service allows you to register a user
        /// </summary>
        /// <param name="registerViewModelInput">View model of login register</param>
        [SwaggerResponse(statusCode: 200, description: "Success in authenticating", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required fields", Type = typeof(ValidateFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(ErrorGenericViewModel))]
        [HttpPost] //Informo que essa rota vai ter o verbo post do http
        [Route("register")]
        [ValidationModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
            
            optionsBuilder.UseSqlServer("Server=localhost;Database=Course;user=sa;password=App@220030");
            CourseDbContext context = new CourseDbContext(optionsBuilder.Options);

            var pendingMigrations =  context.Database.GetPendingMigrations();

            if(pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new UserEntity();
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;
            context.User.Add(user);
            context.SaveChanges();

            return Created("", registerViewModelInput); // Retorna status 201 (created)
        }
    }
}
