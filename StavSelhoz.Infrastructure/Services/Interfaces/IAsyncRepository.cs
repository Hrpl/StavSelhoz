using StavSelhoz.Domain.Commons.Request;
using StavSelhoz.Domain.Commons.Response;
using StavSelhoz.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IAsyncRepository<TDTO, DModel> where TDTO : class where DModel : class
{
    public Task<IEnumerable<TDTO>> GetAsync();
    public Task<int> CreateAsync(DModel model);
    public Task<int> UpdateAsync(DModel model);
    public Task<int> DeleteAsync(int id);
}
