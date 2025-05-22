using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/client")]
[ApiController]
public class ClientsController(IClientService clientService, ILogger<ClientsController> logger, IMapper mapper) : ControllerBase
{
    private readonly IClientService _clientService = clientService;
    private readonly ILogger<ClientsController> _logger = logger;
    private readonly IMapper _mapper = mapper;

    // GET: api/<ClientsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponse>>> Get()
    {
        try
        {
            var response = await _clientService.GetAsync();

            if (response != null) return Ok(response);
            else return BadRequest("Не найдено данных");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching clients.");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. \n {ex.Message}"
            });
        }
    }

    // POST api/<ClientsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ClientResponse request)
    {
        try
        {

            var model = _mapper.Map<ClientModel>(request);
            var result = await _clientService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching clients.");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. \n {ex.Message}"
            });
        }

    }
}
