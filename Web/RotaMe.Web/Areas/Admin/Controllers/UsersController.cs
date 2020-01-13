using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Web.ViewModels.Administration.Users;

namespace RotaMe.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<RotaMeUser> userManager;

        public UsersController(IUsersService usersService, UserManager<RotaMeUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            List<UsersListViewModel> users = await this.usersService
                .GetAllUsers()
                .To<UsersListViewModel>()
                .ToListAsync();

            for (int i = 0; i < users.Count(); i++)
            {
                var user = await userManager.FindByEmailAsync(users[i].Email);
                users[i].Roles = await userManager.GetRolesAsync(user);
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.usersService.GetUserById(id);

            var userDetailsViewModel = new UserDetailsViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                EmailConfirmed = user.EmailConfirmed,
                GenderName = user.Gender.Name,
                LastLoggedIn = user.LastLoggedIn,
                LockoutEnabled = user.LockoutEnabled,
                PhoneNumber = user.PhoneNumber
            };

            var userFromDb = await userManager.FindByEmailAsync(user.Email);

            userDetailsViewModel.Roles = await userManager.GetRolesAsync(userFromDb);

            return View(userDetailsViewModel);
        }
    }
}