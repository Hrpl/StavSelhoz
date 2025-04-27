using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Entities;

public class ProviderProductsEntity : BaseEntity
{
    public int ProviderId { get; set; }
    public int ProductId { get; set; }
}
