using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SMS.DTOS.CourseDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/courses")]
[ApiVersion("1.0")]
public class CourseController : ControllerBase
{

    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }
    //=============================================
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Create Course",Description = "Add a new course")]
    public async Task<IActionResult> CreateCourse(CreateCourseRequestDTO dto)
    {
        return Ok(await _service.CreateCourseAsync(dto));
    }
    //==========================================================

    [HttpGet]
    [EnableRateLimiting("Fixed")]
    [ResponseCache(Duration = 60)]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Get Courses",Description = "Retrieve all courses")]
    public async Task<IActionResult> GetCourses()
    {
        return Ok( await _service.GetAllCoursesAsync());
    }
    //=============================================================================

    [HttpGet("{id}")]
    [EnableRateLimiting("Fixed")]
    [ResponseCache(Duration = 60)]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Get Course By Id", Description = "Retrieve a course using course id")]
    public async Task<IActionResult> GetCourse(int id)
    {
        return Ok(await _service.GetCourseByIdAsync(id));
    }
    //=======================================================================

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Update Course", Description = "Update course details")]
    public async Task<IActionResult> UpdateCourse(int id,UpdateCourseRequestDTO dto)
    {
        return Ok( await _service.UpdateCourseAsync(id, dto));
    }
    //====================================================================
}