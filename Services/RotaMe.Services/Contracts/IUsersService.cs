using RotaMe.Sevices.Models;
using RotaMe.Sevices.Models.Administration.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<ListUserServiceModel> GetAllUsers();

        IQueryable<ListUsersToAssignServiceModel> GetAllUsersToAssign();

        Task<IEnumerable<ListUsersToUnassignServiceModel>> GetAllUsersToUnassign();

        Task<UserDetailsServiceModel> GetUserById(string id);

        Task<bool> Create(UserCreateServiceModel userCreateServiceModel);

    }
}
