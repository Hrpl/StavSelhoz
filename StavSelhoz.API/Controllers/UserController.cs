
using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Compilers;
using SqlKata.Execution;
using Swashbuckle.AspNetCore.Annotations;

namespace EMDR42.API.Controllers;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IMapper mapper,
        IUserRepository userService,
        ILogger<UserController> logger
        )
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger;
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Создание нового пользователя в системе")]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest req)
    {
        if (string.IsNullOrEmpty(req.Email))
        {
            _logger.LogError("Поле Email не заполнено");
            return BadRequest(new ProblemDetails
            {
                Title = "BadRequest",
                Detail = "Поле Email не заполнено"
            });
        }

        var findEmail = await _userService.CheckedUserByLoginAsync(req.Email);

        if (findEmail)
        {
            _logger.LogError("Пользователь с таким email уже существует");
            return BadRequest(new ProblemDetails
            {
                Title = "BadRequest",
                Detail = "Пользователь с таким email уже существует"
            });
        }

        var user = _mapper.Map<UserModel>(req);

        try
        {
            var resUser = await _userService.CreatedUserAsync(user);
            if (resUser != 1) {
                return BadRequest(new ProblemDetails
                {
                    Title = "BadRequest",
                    Detail = "Ошибка создания пользователя"
                });
            }

            return Ok(req.Email);
        }
        catch (Exception ex)
        {
            await _userService.DeleteUserAsync(user.Email);
            _logger.LogError($"Ошибка при создании пользователя: \n {ex.Message} \n {ex.StackTrace}");
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal server error",
                Detail = $"Произошла ошибка при обработке запроса. {ex.Message}"
            }); 

        }
    }
}
