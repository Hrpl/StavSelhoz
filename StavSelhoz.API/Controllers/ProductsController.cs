using Microsoft.AspNetCore.Mvc;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/product")]
[ApiController]
public class ProductsController(IProductService productService, ILogger<ProductsController> logger, IMapper mapper) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly ILogger<ProductsController> _logger = logger;
    private readonly IMapper _mapper = mapper;

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> Get()
    {
        try
        {
            var response = await _productService.GetAsync();

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

    // POST api/<ProductsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductResponse request)
    {
        try
        {

            var model = _mapper.Map<ProductModel>(request);
            var result = await _productService.CreateAsync(model);

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
