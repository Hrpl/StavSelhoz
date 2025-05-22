using StavSelhoz.Domain.Commons.Response;
namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IJwtHelper
{
    public JwtResponse CreateJwtAsync(int userId);
    public Task<int> DecodJwt(string accessToken);
}
