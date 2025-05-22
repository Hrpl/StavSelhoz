using SqlKata.Execution;
using StavSelhoz.Domain.Commons.Request;
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
    public async Task<int> CreateOrder(OrderModel model)
    {
        return _query.Query("orders").InsertGetId<int>(model);
    }

    public async Task CreateProductInOrder(IEnumerable<OrderProductModel> model)
    {
        //n+1 error
        foreach (var product in model)
        {
            var query = _query.Query("order_products").AsInsert(product);
            await _query.ExecuteAsync(query);
        }
    }

    public async Task<IEnumerable<OrderResponse>> GetOrders()
    {
        var query = _query.Query("orders as o")
            .Join("order_status as os", "os.id", "o.order_status_id")
            .Where("o.created_at", ">", DateTime.Now.AddMonths(-1))
            .Select("o.id as Id", "o.user_id as UserId", "os.name as OrderStatus", "o.created_at as Date");

        var orders = await query.GetAsync<OrderResponse>();

        var orderIds = orders.Select(o => o.Id).ToList();

        var orderProducts = await _query.Query("order_products")
            .Join("products", "order_products.product_id", "products.id")
            .Select(
                "order_products.order_id as OrderId",
                "products.id as Id",
                "products.name as Name",
                "products.code as Code",
                "products.measure as Measure",
                "products.price as Price"
            )
            .WhereIn("order_products.order_id", orderIds)
            .GetAsync<OrderProductResponse>();

        var productsByOrder = orderProducts
            .GroupBy(op => op.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var order in orders)
        {
            if (productsByOrder.TryGetValue(order.Id, out var products))
            {
                order.Products = products;
                order.SummaryPrice = products.Sum(p => p.Price);
            }
            else
            {
                order.Products = new List<OrderProductResponse>();
                order.SummaryPrice = 0;
            }
        }

        return orders;
    }

    public async Task<IEnumerable<OrderStatusResponse>> GetOrderStatus()
    {
        var query = _query.Query("orders_status")
            .Select("id as Id",
            "name as Name");

        var result = await _query.GetAsync<OrderStatusResponse>(query);

        return result;
    }

    public async Task<ReportStatusOrder> GetReportStatus()
    {
        var result = await _query.Query("orders")
        .Where("created_at", ">", DateTime.Now.AddMonths(-1))
        .SelectRaw("COUNT(CASE WHEN order_status_id = 1 THEN 1 END) as New")
        .SelectRaw("COUNT(CASE WHEN order_status_id = 2 THEN 1 END) as InProcess")
        .SelectRaw("COUNT(CASE WHEN order_status_id = 3 THEN 1 END) as Completed")
        .FirstOrDefaultAsync<ReportStatusOrder>();

        return result ?? new ReportStatusOrder();
    }
}
