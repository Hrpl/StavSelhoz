using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class OrderProductModel
{
    [SqlKata.Column("order_id")]
    public int OrderId { get; set; }
    [SqlKata.Column("product_id")]
    public int ProductId { get; set; }
    [SqlKata.Column("count")]
    public int Count { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}
