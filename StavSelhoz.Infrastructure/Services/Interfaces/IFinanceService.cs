using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IFinanceService : IAsyncRepository<FinanceResponse, FinanceModel>
{
    public Task<FinanceStatResponse> GetFinanceSummary();
}
