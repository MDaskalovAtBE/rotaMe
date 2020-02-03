﻿using Microsoft.AspNetCore.Identity;
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
    }
}
