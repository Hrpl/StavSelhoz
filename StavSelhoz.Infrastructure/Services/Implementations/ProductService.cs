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

public class ProductService : IProductService
{
    private readonly QueryFactory _query;
    private readonly string TableName = "products";
    public ProductService(IDbConnectionManager connectionManager)
    {
        _query = connectionManager.PostgresQueryFactory;
    }

    public async Task<int> CreateAsync(ProductModel model)
    {
        var query = _query.Query(TableName).AsInsert(model);

        return await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<ProductResponse>> GetAsync()
    {
        var query = _query.Query(TableName)
            .Select("id as Id", 
            "name as Name",
            "code as Code",
            "measure as Measure",
            "price as Price",
            "in_storage as InStorage");

        var result = await _query.GetAsync<ProductResponse>(query);
        return result;
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(ProductModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductInStorageResponse>> GetProductInStorageAsync()
    {
        var query = _query.Query(TableName)
            .Select("name as Name", "in_storage as InStorage");

        var result = await _query.GetAsync<ProductInStorageResponse>(query);
        return result;
    }
}
