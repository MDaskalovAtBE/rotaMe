using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<UserServiceModel> GetAllUsers();
    }
}
