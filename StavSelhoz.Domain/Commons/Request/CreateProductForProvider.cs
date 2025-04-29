using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Request;

public class CreateProductForProvider
{
    public int ProviderId { get; set; }
    public int ProductId { get; set; }
}
