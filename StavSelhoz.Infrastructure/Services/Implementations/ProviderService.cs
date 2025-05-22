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

    public async Task CreateProductForProviderAsync(IEnumerable<ProviderProductsModel> model)
    {
        foreach (var product in model)
        {

            var query = _query.Query("provider_products").AsInsert(product);
            await _query.ExecuteAsync(query);
        }
    }

    public async Task<int> CreateProviderAsync(ProviderModel model)
    {
        var id = _query.Query("providers").InsertGetId<int>(model);
        return id;
    }

    public async Task<IEnumerable<ProviderResponse>> GetAsync()
    {
        var suppliersQuery = _query.Query("providers")
            .Select("id", "company_name as CompanyName", "contact_people as ContactPeople", "сonditions as Conditions");

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
