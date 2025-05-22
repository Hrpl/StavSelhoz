using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Response;

public class ReportStatusOrder
{
    public int Completed { get; set; }
    public int InProccess { get; set; }
    public int New {  get; set; }
}
