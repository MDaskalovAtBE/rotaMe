using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Web.ViewModels.Administration.Users;

namespace RotaMe.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> List()
        {
            List<UsersListViewModel> users = await this.usersService
                .GetAllUsers()
                .To<UsersListViewModel>()
                .ToListAsync();

            return View(users);
        }
    }
}