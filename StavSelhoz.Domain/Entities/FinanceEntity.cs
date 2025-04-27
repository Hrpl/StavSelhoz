using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Entities;

public class FinanceEntity : BaseEntity
{
    public string Type { get; set; }
    public double Summary { get; set; }
    public string ResponsiblePerson { get; set; }
}
