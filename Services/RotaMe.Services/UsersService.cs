using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services
{
    public class UsersService : IUsersService
    {
        private readonly RotaMeDbContext context;

        public UsersService(RotaMeDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ListUserServiceModel> GetAllUsers()
        {
            var users = this.context.Users;
            return users.To<ListUserServiceModel>();
        }

        public async Task<UserDetailsServiceModel> GetUserById(string userId)
        {
            var user = await this.context.Users.To<UserDetailsServiceModel>().SingleOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user;
        }
    }
}
