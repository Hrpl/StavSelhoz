using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IProviderService
{
    public Task<IEnumerable<ProviderResponse>> GetAsync();
    public Task<int> CreateProviderAsync(ProviderModel model);
    public Task CreateProductForProviderAsync(IEnumerable<ProviderProductsModel> model);
}
