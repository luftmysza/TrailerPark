using Microsoft.AspNetCore.Mvc;

using TrailerPark.Core.Models;
using TrailerPark.Application.Services;

namespace TrailerPark.API.Controllers;

[ApiController]
[Route("/")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _service;

    public MoviesController(MovieService service)
    {
         _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] MovieQuery movieQuery)
    {   
        if (movieQuery is null) 
            return BadRequest("Provided parameters do not meet API requirements");

        IEnumerable<Movie?>? result = null!;

        try
        {
            result = await _service.Inbound(movieQuery);
        }
        catch (Exception)
        {
            return ValidationProblem();
        }
        
        if (result is null)
        {
            return NoContent();
        }
        
        return Ok(result);
    }
}