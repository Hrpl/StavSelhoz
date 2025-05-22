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
    [HttpPost("{id}")]
    public async Task<ActionResult> Post([FromBody] IEnumerable<CreateProductInOrder> request, int id)
    {
        try
        {
            var orderModel = new OrderModel { OrderStatusId = 1, UserId = id};
            var orderId = await _orderService.CreateOrder(orderModel);

            List<OrderProductModel> productModel = new List<OrderProductModel>();

            foreach (var product in request)
            {
                var model = _mapper.Map<OrderProductModel>(product);
                model.OrderId = orderId;
                productModel.Add(model);
            }

            await _orderService.CreateProductInOrder(productModel);

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

    /*[HttpPost("product")]
    public async Task<ActionResult> PostProducts()
    {
        try
        {
            

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
    }*/
}
