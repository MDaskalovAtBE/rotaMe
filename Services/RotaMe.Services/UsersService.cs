using RotaMe.Data;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Services
{
    public class UsersService : IUsersService
    {
        private readonly RotaMeDbContext context;

        public UsersService(RotaMeDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UserServiceModel> GetAllUsers()
        {
            var users = this.context.Users;
            return users.To<UserServiceModel>();
        }
    }
}
