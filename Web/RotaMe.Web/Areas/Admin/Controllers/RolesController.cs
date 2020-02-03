using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Web.ViewModels.Administration.Roles;

namespace RotaMe.Web.Areas.Admin.Controllers
{
    public class RolesController : AdminController
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public async Task<IActionResult> List()
        {
            var roles = await rolesService.GetAllRoles();
            var listRoleViewModel = roles.Select(r => new RolesListViewModel()
            {
                Id = r.Id,
                Name = r.Name + "s",
                UsersCount = r.UsersCount,
                Users = r.Users.Select(u => new RoleListUsersViewModel() {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = u.Avatar,
                    LastLoggedIn = u.LastLoggedIn,
                    LockoutEnabled = u.LockoutEnabled
                }).ToList()
            }).ToList();

            for (int i = 0; i < listRoleViewModel.Count; i++)
            {
                switch (listRoleViewModel[i].Name)
                {
                    case "Users":
                        listRoleViewModel[i].CardIcon = "fa-user";
                        break;
                    case "Admins":
                        listRoleViewModel[i].CardIcon = "fa-user-tie";
                        break;
                    case "Owners":
                        listRoleViewModel[i].CardIcon = "fa-user-alt";
                        break;
                    default:
                        listRoleViewModel[i].CardIcon = "fa-user";
                        break;
                }
            }

            return View(listRoleViewModel);
        }
    }
}