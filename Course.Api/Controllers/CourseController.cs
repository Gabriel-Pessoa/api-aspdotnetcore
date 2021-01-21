using Course.Api.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Course.Api.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        /// <summary>
        /// This service allows register course for the authenticated user
        /// </summary>
        /// <param name="courseViewModelInput"></param>
        /// <returns>Returns status 201 and user data</returns>
        [SwaggerResponse(statusCode: 201, description: "Success to the register a course")]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", courseViewModelInput);
        }

        /// <summary>
        /// This service get all the activities courses of the user 
        /// </summary>
        /// <returns>Returns status ok and data of course of user</returns>
        [SwaggerResponse(statusCode: 200, description: "Success to the get all the courses of user")]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var courses = new List<CourseViewModelOutput>();

            //var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            courses.Add(new CourseViewModelOutput()
            {
                Login = "",
                Description = "teste",
                Name = "teste"
            });

            return Ok(courses);
        }
    }
}
