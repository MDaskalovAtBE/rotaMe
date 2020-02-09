using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using RotaMe.Web.InputModels.Administration.Roles;
using RotaMe.Web.ViewModels.Administration.Roles;
using RotaMe.Web.ViewModels.Administration.Users;

namespace RotaMe.Web.Areas.Admin.Controllers
{
    public class RolesController : AdminController
    {
        private readonly IRolesService rolesService;
        private readonly IUsersService usersService;

        public RolesController(IRolesService rolesService, IUsersService usersService)
        {
            this.rolesService = rolesService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateInputModel roleCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var roleCreateServiceModel = new RoleCreateServiceModel()
            {
                Name = roleCreateInputModel.Name,
                NarmalizedName = roleCreateInputModel.Name.ToUpper()
            };

            var result = await rolesService.Create(roleCreateServiceModel);

            if (!result)
            {
                return this.View();
            }

            return this.Redirect("/admin/roles/list");
        }

        [HttpGet]
        public IActionResult Assign()
        {
            var users = usersService.GetAllUsersToAssign().To<UsersListToAssignViewModel>();
            var roles = rolesService.GetAllRolesToAssign().To<RolesListToAssignViewModel>();

            this.ViewData["users"] = users;
            this.ViewData["roles"] = roles;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(RoleAssignToUserInputModel roleAssignToUserInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var roleAssignTotUserServiceModel = new RoleAssignToUserServiceModel()
            {
                UserId = roleAssignToUserInputModel.UserId,
                RoleName = roleAssignToUserInputModel.RoleName
            };

            var result = await rolesService.AssignRoleToUser(roleAssignTotUserServiceModel);

            if (!result)
            {
                return this.View();
            }

            return this.Redirect("/admin/roles/list");
        }

        [HttpGet]
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
                        listRoleViewModel[i].DeleteButton = false;
                        break;
                    case "Admins":
                        listRoleViewModel[i].CardIcon = "fa-user-tie";
                        listRoleViewModel[i].DeleteButton = false;
                        break;
                    case "Owners":
                        listRoleViewModel[i].CardIcon = "fa-user-alt";
                        listRoleViewModel[i].DeleteButton = false;
                        break;
                    default:
                        listRoleViewModel[i].CardIcon = "fa-user";
                        listRoleViewModel[i].DeleteButton = true;
                        break;
                }
            }

            return View(listRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var isRoleDeleted = await rolesService.Delete(id);

            if (isRoleDeleted)
            {
                return this.StatusCode(200);
            }

            return this.StatusCode(400);
        }
    }
}