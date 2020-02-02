using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
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
    public class RolesService : IRolesService
    {
        private readonly RotaMeDbContext context;
        private readonly UserManager<RotaMeUser> userManager;

        public RolesService(RotaMeDbContext context, UserManager<RotaMeUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ListRoleServiceModel>> GetAllRoles()
        {
            var users = this.context.Users;


            var roles = this.context.Roles;
            var listRoleServiceModel = await roles.To<ListRoleServiceModel>().ToListAsync();

            for (int i = 0; i < listRoleServiceModel.Count(); i++)
            {
                var userInRole = await userManager.GetUsersInRoleAsync(listRoleServiceModel[i].Name);
                listRoleServiceModel[i].UsersCount = userInRole.Count();
            }

            return listRoleServiceModel;
        }
    }
}
