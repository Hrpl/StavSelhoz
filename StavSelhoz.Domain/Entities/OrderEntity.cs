using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public int OrderStatusId { get; set; }
    public double SummaryPrice { get; set; }
}
