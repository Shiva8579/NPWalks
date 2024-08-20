using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NPWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Tim", "Jones", "Rahul", "David" };
            return Ok(studentNames);
        }
    }
}
 