using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StavSelhoz.Controllers;

[Route("api/report")]
[ApiController]
public class ReportsController(IOrderService orderService, IFinanceService financeService, IProductService productService, ILogger<ReportsController> logger) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly IFinanceService _financeService = financeService;
    private readonly IProductService _productService = productService;
    private readonly ILogger<ReportsController> _logger = logger;


    [HttpGet("sales")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetSales()
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

    [HttpGet("status")]
    public async Task<ActionResult<ReportStatusOrder>> GetStatus()
    {
        try
        {
            var response = await _orderService.GetReportStatus();

            return Ok(response);
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

    [HttpGet("finance")]
    public async Task<ActionResult<FinanceStatResponse>> GetFinance()
    {
        try
        {
            var response = await _financeService.GetFinanceSummary();

            return Ok(response);
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

    [HttpGet("product/storage")]
    public async Task<ActionResult<ProductInStorageResponse>> GetProductInStorage()
    {
        try
        {
            var response = await _productService.GetProductInStorageAsync();

            return Ok(response);
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
