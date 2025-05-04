
using StavSelhoz.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class DbConnectionManager : IDbConnectionManager
{ 
    private readonly ILogger<DbConnectionManager> _logger;

    public DbConnectionManager(ILogger<DbConnectionManager> logger)
    {
        _logger = logger;
    }
    private string NpgsqlConnectionString => $"Host=localhost;Port=5432;Database=StavSelhoz;Username=postgres;Password=postgres;";


    public NpgsqlConnection PostgresDbConnection => new(NpgsqlConnectionString);

    public QueryFactory PostgresQueryFactory => new(PostgresDbConnection, new PostgresCompiler(), 60)
    {
        Logger = compiled => { _logger.LogInformation("Query = {@Query}", compiled.ToString()); }
    };
}
