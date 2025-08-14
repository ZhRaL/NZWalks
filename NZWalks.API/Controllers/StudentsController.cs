using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    // GET
    [HttpGet]
    public IActionResult GetAllStudents()
    {
       string[] studentNames = ["a", "b", "c"];
       
       return Ok(studentNames);
    }
}