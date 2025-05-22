using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Entities;

public class OrderProductsEntity : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
}
