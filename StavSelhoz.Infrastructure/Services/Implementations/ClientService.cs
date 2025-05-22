using SqlKata;
using SqlKata.Execution;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class ClientService : IClientService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "clients";

    public ClientService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<int> CreateAsync(ClientModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        return await _query.ExecuteAsync(query);
    }
    public Task<IEnumerable<ClientResponse>> GetAsync()
    {
        var query = _query.Query(TableName)
            .Select("id as Id",
            "company_name as CompanyName",
            "concact_people as ConcactPeople",
            "phone_number as PhoneNumber",
            "email as Email",
            "address as Address");

        var result = _query.GetAsync<ClientResponse>(query);
        return result;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task<int> UpdateAsync(ClientModel model)
    {
        throw new NotImplementedException();
    }
}
