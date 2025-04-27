using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Infrastructure.Services.Implementations;
using StavSelhoz.Infrastructure.Services.Interfaces;
using StavSelhoz.Domain.Commons.Response;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace StavSelhoz.API.Controllers;

[Route("user")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtHelper _jwtHelper;
    private readonly IUserRepository _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IJwtHelper jwtHelper,
        IMapper mapper,
        IUserRepository userService,
        ILogger<AuthController> logger)
    {
        _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger;
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Авторизация пользователя")]
    public async Task<ActionResult<JwtResponse>> Auth(LoginRequest req)
    {

        try
        {
            var check = await _userService.LoginUserAsync(req);

            if (!check)
            {
                _logger.LogError("Неверный логин или пароль");
                return BadRequest(new ProblemDetails
                {
                    Title = "BadRequest",
                    Detail = "Неверный логин или пароль"
                });
            }

            var id = await _userService.GetUserIdAsync(req.Email);
            var jwt = _jwtHelper.CreateJwtAsync(id);

            return Ok(jwt);
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
