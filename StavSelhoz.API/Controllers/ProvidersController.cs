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

[Route("api/providers")]
[ApiController]
public class ProvidersController(IProviderService providerService, ILogger<ProvidersController> logger, IMapper mapper) : ControllerBase
{
    private readonly IProviderService _providerService = providerService;
    private readonly ILogger<ProvidersController> _logger = logger;
    private readonly IMapper _mapper = mapper;

    // GET: api/<ProvidersController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProviderResponse>>> Get()
    {
        try
        {
            var response = await _providerService.GetAsync();

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


    // POST api/<ProvidersController>
    [HttpPost]
    public async Task<ActionResult> CreateProvider([FromBody] CreateProviderRequest request)
    {
        try
        {

            var providerModel = _mapper.Map<ProviderModel>(request);
            var providerId = await _providerService.CreateProviderAsync(providerModel);

            List<ProviderProductsModel> productsModel = new List<ProviderProductsModel>();
            foreach (var item in request.Products)
            {
                var model = _mapper.Map<ProviderProductsModel>(item);
                model.ProviderId = providerId;
                productsModel.Add(model);
            }

            await _providerService.CreateProductForProviderAsync(productsModel);

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
