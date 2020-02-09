using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Sevices.Models;
using RotaMe.Web.InputModels.Administration.Roles;
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