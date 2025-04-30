using SqlKata.Execution;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using StavSelhoz.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Implementations;

public class OrderService(IDbConnectionManager connectionManager) : IOrderService
{
    private readonly QueryFactory _query = connectionManager.PostgresQueryFactory;
    public async Task CreateOrder(OrderModel model)
    {
        var query = _query.Query("orders").AsInsert(model);
        await _query.ExecuteAsync(query);
    }

    public async Task CreateProductInOrder(OrderProductModel model)
    {
        var query = _query.Query("order_products").AsInsert(model);
        await _query.ExecuteAsync(query);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrders()
    {
        var query = _query.Query("orders as o")
            .Join("order_status as os", "os.id", "o.order_status_id")
            .Select("o.id as Id", "o.user_id as UserId", "os.name as OrderStatus");

        var suppliers = await query.GetAsync<OrderResponse>();

        foreach (var supplier in suppliers)
        {
            supplier.Products = await _query.Query("order_products")
                .Join("products", "order_products.product_id", "products.id")
                .Select(
                    "products.id as Id ",
                    "products.name as Name",
                    "products.code as Code",
                    "products.measure as Measure",
                    "products.price as Price"
                )
                .Where("order_products.order_id", supplier.Id)
                .GetAsync<OrderProductResponse>();
        }

        return suppliers;
    }

    public async Task<IEnumerable<OrderStatusResponse>> GetOrderStatus()
    {
        var query = _query.Query("orders_status")
            .Select("id as Id",
            "name as Name");

        var result = await _query.GetAsync<OrderStatusResponse>(query);

        return result;
    }
}
