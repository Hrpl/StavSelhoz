using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Request;

public class CreateProviderRequest
{
    public string CompanyName { get; set; }
    public string ContactPeople { get; set; }
    public string Сonditions { get; set; }
    public IEnumerable<CreateProductForProvider> Products { get; set; }
}
