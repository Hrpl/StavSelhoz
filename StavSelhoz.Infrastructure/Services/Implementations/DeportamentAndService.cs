using SqlKata.Execution;
using StavSelhoz.Domain.Commons.DTO;
using StavSelhoz.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class DeportamentAndService(IDbConnectionManager connectionManager) : IDeportamentAndRoleService
{
    private readonly QueryFactory _query = connectionManager.PostgresQueryFactory;

    public async Task<IEnumerable<GetDeportamentDTO>> GetDeportaments()
    {
        var query = _query.Query("deportaments")
            .Select("id as Id", "name as Name");

        var result = await _query.GetAsync<GetDeportamentDTO>(query);

        return result;
    }

    public async Task<IEnumerable<GetRolesDTO>> GetRoles()
    {
        var query = _query.Query("roles")
            .Select("id as Id", "name as Name");

        var result = await _query.GetAsync<GetRolesDTO>(query);

        return result;
    }
}
