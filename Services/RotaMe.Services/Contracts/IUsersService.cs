using RotaMe.Sevices.Models;
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

        Task<UserDetailsServiceModel> GetUserById(string id);

        Task<bool> Create(UserCreateServiceModel userCreateServiceModel);

    }
}
