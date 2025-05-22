using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Entities;
using StavSelhoz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task<int> CreateOrder(OrderModel model);
    public Task CreateProductInOrder(IEnumerable<OrderProductModel> model);
    public Task<IEnumerable<OrderStatusResponse>> GetOrderStatus();
    public Task<IEnumerable<OrderResponse>> GetOrders();
    public Task<ReportStatusOrder> GetReportStatus();
}
