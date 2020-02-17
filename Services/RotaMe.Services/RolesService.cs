using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using RotaMe.Sevices.Models.Administration.Roles;
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

        public async Task<bool> Create(RoleCreateServiceModel roleCreateServiceModel)
        {
            var roles = await this.context.Roles.ToListAsync();
            var role = roles.FirstOrDefault(r => r.NormalizedName == roleCreateServiceModel.NarmalizedName);
            if (role == null)
            {
                var result = await context.Roles.AddAsync(new IdentityRole()
                {
                    Name = roleCreateServiceModel.Name,
                    NormalizedName = roleCreateServiceModel.NarmalizedName
                });

                context.SaveChanges();

                return false ? result == null : true;
            }

            return false;
        }

        public async Task<bool> Delete(string id)
        {
            var role = await context.Roles.FindAsync(id);

            var result = context.Roles.Remove(role);

            context.SaveChanges();

            return false ? result == null : true;
        }

        public IQueryable<ListRolesToAssignServiceModel> GetAllRolesToAssign()
        {
            return this.context.Roles.To<ListRolesToAssignServiceModel>();
        }

        public async Task<IEnumerable<ListRoleServiceModel>> GetAllRoles()
        {
            var users = this.context.Users;


            var roles = this.context.Roles;
            var listRoleServiceModel = await roles.To<ListRoleServiceModel>().ToListAsync();

            for (int i = 0; i < listRoleServiceModel.Count(); i++)
            {
                var usersInRole = await userManager.GetUsersInRoleAsync(listRoleServiceModel[i].Name);
                var listRoleUserServiceModels = usersInRole.Select(u => new ListRoleUserServiceModel() { 
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = u.Avatar,
                    LastLoggedIn = u.LastLoggedIn,
                    LockoutEnabled = u.LockoutEnabled
                }).ToList();

                listRoleServiceModel[i].Users = listRoleUserServiceModels;
                listRoleServiceModel[i].UsersCount = usersInRole.Count();
            }

            return listRoleServiceModel;
        }

        public async Task<bool> AssignRoleToUser(RoleAssignToUserServiceModel roleAssignToUserServiceModel)
        {
            var user = await userManager.FindByIdAsync(roleAssignToUserServiceModel.UserId);
            var result = await userManager.AddToRoleAsync(user, roleAssignToUserServiceModel.RoleName);

            return false ? result == null : true;
        }

        public async Task<bool> UnassignRoleFromUser(RoleUnassignFromUserServiceModel roleUnassignFromUserServiceModel)
        {
            var user = await userManager.FindByIdAsync(roleUnassignFromUserServiceModel.UserId);
            var result = await userManager.RemoveFromRoleAsync(user, roleUnassignFromUserServiceModel.RoleName);

            return false ? result == null : true;
        }
    }
}
