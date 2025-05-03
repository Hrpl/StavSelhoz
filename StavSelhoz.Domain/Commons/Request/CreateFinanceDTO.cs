using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Request;

public class CreateFinanceDTO
{
    public string Type { get; set; }
    public double Summary { get; set; }
    public string ResponsiblePerson { get; set; }
}
