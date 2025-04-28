using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IReportService
{
    public void SalesReport();
    public void CompletedOrder();
    public void FinancesReport();
    public void StoreProductReport();
}
