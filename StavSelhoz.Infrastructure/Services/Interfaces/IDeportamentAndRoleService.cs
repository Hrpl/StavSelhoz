using StavSelhoz.Domain.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IDeportamentAndRoleService
{
    public Task<IEnumerable<GetRolesDTO>> GetRoles();
    public Task<IEnumerable<GetDeportamentDTO>> GetDeportaments();
}
