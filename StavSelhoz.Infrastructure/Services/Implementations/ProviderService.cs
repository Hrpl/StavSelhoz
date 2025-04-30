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

public class ProviderService(IDbConnectionManager connectionManager) : IProviderService
{
    private readonly QueryFactory _query = connectionManager.PostgresQueryFactory;

    public async Task CreateProductForProviderAsync(ProviderProductsModel model)
    {
        var query = _query.Query("provider_products").AsInsert(model);
        await _query.ExecuteAsync(query);
    }

    public async Task CreateProviderAsync(ProviderModel model)
    {
        var query = _query.Query("providers").AsInsert(model);
        await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<ProviderResponse>> GetAsync()
    {
        var suppliersQuery = _query.Query("providers")
            .Select("id", "company_name as CompanyName", "contact_people as ContactPeople", "conditions as Conditions");

        var suppliers = await suppliersQuery.GetAsync<ProviderResponse>();

        foreach (var supplier in suppliers)
        {
            supplier.Products = await _query.Query("provider_products")
                .Join("products", "provider_products.product_id", "products.id")
                .Select(
                    "products.id as Id",
                    "products.name as Name",
                    "products.code as Code",
                    "products.measure as Measure",
                    "products.price as Price"
                )
                .Where("provider_products.provider_id", supplier.Id)
                .GetAsync<ProductProviderResponse>();
        }

        return suppliers;
    }
}
