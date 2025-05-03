using SqlKata.Execution;
using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class FinanceService : IFinanceService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "finances";
    public FinanceService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }
    public async Task<int> CreateAsync(FinanceModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<FinanceResponse>> GetAsync()
    {
        var query = _query.Query(TableName)
            .Select("type as Type",
            "summary as Summary",
            "responsible_person as ResponsiblePerson",
            "created_at as CreatedAt");

        var result = await _query.GetAsync<FinanceResponse>(query);
        return result;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(FinanceModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<FinanceStatResponse> GetFinanceSummary()
    {
        var result = await _query.Query("finances")
        .Where("created_at", ">", DateTime.Now.AddMonths(-1))
        .SelectRaw("SUM(CASE WHEN type = 'income' THEN summary ELSE 0 END) as Income")
        .SelectRaw("SUM(CASE WHEN type = 'expenses' THEN summary ELSE 0 END) as Expenses")
        .FirstOrDefaultAsync<FinanceStatResponse>();

        return result ?? new FinanceStatResponse();
    }
}
