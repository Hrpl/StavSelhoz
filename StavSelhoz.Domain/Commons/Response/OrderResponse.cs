using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Response;

public class OrderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime Date { get; set; }
    public double SummaryPrice { get; set; }
    public IEnumerable<OrderProductResponse> Products { get; set; }
}
