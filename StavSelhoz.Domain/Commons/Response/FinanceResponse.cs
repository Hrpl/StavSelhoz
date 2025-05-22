using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Response;

public class FinanceResponse
{
    public string Type { get; set; }
    public double Summary { get; set; }
    public string ResponsiblePerson { get; set; }
    public DateTime CreatedAt { get; set; }
}
