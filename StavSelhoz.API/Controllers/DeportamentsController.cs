using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StavSelhoz.Domain.Commons.DTO;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/deportaments")]
[ApiController]
public class DeportamentsController(IDeportamentAndRoleService deportamentAndRoleService, ILogger<DeportamentsController> logger) : ControllerBase
{
    private readonly IDeportamentAndRoleService _deportamentAndRoleService = deportamentAndRoleService;
    private readonly ILogger<DeportamentsController> _logger = logger;

    // GET: api/<RolesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDeportamentDTO>>> Get()
    {
        try
        {
            var response = await _deportamentAndRoleService.GetDeportaments();

            if (response != null) return Ok(response);
            else return BadRequest("Ошибка получения данных");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Произошла ошибка при обработке запроса: \n {ex.Message} \n {ex.StackTrace}");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. {ex.Message}"
            });
        }
    }
}
