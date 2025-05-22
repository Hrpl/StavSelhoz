using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Response;

public class ProviderResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string ContactPeople { get; set; }
    public string Conditions { get; set; }
    public IEnumerable<ProductProviderResponse> Products { get; set; }
}
