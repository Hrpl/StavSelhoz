using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/finance")]
[ApiController]
public class FinancesController(IFinanceService financeService, ILogger<FinancesController> logger, IMapper mapper) : ControllerBase
{
    private readonly IFinanceService _financeService = financeService;
    private readonly ILogger<FinancesController> _logger = logger;
    private readonly IMapper _mapper = mapper;
    // GET: api/<FinancesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinanceResponse>>> Get()
    {
        try
        {
            var response = await _financeService.GetAsync();

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

    // POST api/<FinancesController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateFinanceDTO request)
    {
        try
        {

            var model = _mapper.Map<FinanceModel>(request);
            model.CreatedAt = DateTime.Now;
            var result = await _financeService.CreateAsync(model);

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
