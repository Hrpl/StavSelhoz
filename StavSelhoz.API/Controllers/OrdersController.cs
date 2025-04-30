using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/order")]
[ApiController]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger, IMapper mapper) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly ILogger<OrdersController> _logger = logger;
    private readonly IMapper _mapper = mapper;


    // GET: api/<OrdersController>
    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<OrderStatusResponse>>> GetStatus()
    {
        try
        {
            var response = await _orderService.GetOrderStatus();

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

    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> Get()
    {
        try
        {
            var response = await _orderService.GetOrders();

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

    // POST api/<OrdersController>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Post()
    {
        try
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Unauthorized",
                    Detail = "Invalid user ID in token."
                });
            }
            var model = new OrderModel { OrderStatusId = 1, UserId = Convert.ToInt32(userId)};
            await _orderService.CreateOrder(model);

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

    [HttpPost("product")]
    public async Task<ActionResult> PostProducts([FromBody] CreateProductInOrder request)
    {
        try
        {
            var model = _mapper.Map<OrderProductModel>(request);
            await _orderService.CreateProductInOrder(model);

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
