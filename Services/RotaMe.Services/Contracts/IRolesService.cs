using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services.Contracts
{
    public interface IRolesService
    {
        Task<IEnumerable<ListRoleServiceModel>> GetAllRoles();

        Task<bool> Create(RoleCreateServiceModel roleCreateServiceModel);

        Task<bool> Delete(string id);
    }
}
