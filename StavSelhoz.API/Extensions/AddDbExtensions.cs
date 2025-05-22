
using StavSelhoz.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EMDR42.API.Extensions;

public static class AddDbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            $"Host=localhost;Port=5432;Database=StavSelhoz;Username=postgres;Password=postgres;"
        ));
        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
