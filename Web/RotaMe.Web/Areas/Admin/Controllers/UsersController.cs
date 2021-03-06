﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using RotaMe.Sevices.Models.Administration.Users;
using RotaMe.Web.InputModels.Administration.Users;
using RotaMe.Web.ViewModels.Administration.Users;
using RotaMe.Web.ViewModels.Identity.Register;

namespace RotaMe.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<RotaMeUser> userManager;
        private readonly IRegisterService registerService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly string avatarFolder = "profile-pictures";

        public UsersController(IUsersService usersService, UserManager<RotaMeUser> userManager, IRegisterService registerService, ICloudinaryService cloudinaryService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.registerService = registerService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
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

            return this.View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await this.usersService.GetUserById(id);

            var userDetailsViewModel = new UserDetailsViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Avatar = user.Avatar,
                EmailConfirmed = user.EmailConfirmed,
                GenderName = user.Gender.Name,
                LastLoggedIn = user.LastLoggedIn,
                LockoutEnabled = user.LockoutEnabled,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay
            };

            var userFromDb = await userManager.FindByEmailAsync(user.Email);

            userDetailsViewModel.Roles = await userManager.GetRolesAsync(userFromDb);

            var allGenders = this.registerService.GetAllGenders();

            this.ViewData["genders"] = await allGenders.To<RegisterGenderViewModel>().ToListAsync();
            this.ViewData["userDetails"] = userDetailsViewModel;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string id, UserEditInputModel userEditInputModel)
        {
            string pictureUrl = null;

            if (userEditInputModel.Avatar != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    userEditInputModel.Avatar,
                    userEditInputModel.UserName,
                    avatarFolder);
            }

            var userEditServiceModel = new UserEditServiceModel()
            {
                Id = id,
                Email = userEditInputModel.Email,
                FirstName = userEditInputModel.FirstName,
                LastName = userEditInputModel.LastName,
                UserName = userEditInputModel.UserName,
                Avatar = pictureUrl,
                Gender = userEditInputModel.Gender,
                PhoneNumber = userEditInputModel.PhoneNumber,
                BirthDay = userEditInputModel.BirthDay
            };

            var result = await usersService.Edit(userEditServiceModel);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allGenders = this.registerService.GetAllGenders();

            this.ViewData["genders"] = await allGenders.To<RegisterGenderViewModel>().ToListAsync();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateInputModel userCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allGenders = this.registerService.GetAllGenders();

                this.ViewData["genders"] = await allGenders.To<RegisterGenderViewModel>().ToListAsync();

                return this.View();
            }
            var emailExist = await userManager.Users.FirstOrDefaultAsync(u => u.Email == userCreateInputModel.Email);
            var userNameExist = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userCreateInputModel.UserName);

            if (emailExist != null)
            {
                return this.Redirect("/admin/users/create");
            }
            if (userNameExist != null)
            {
                return this.Redirect("/admin/users/create");
            }

            string pictureUrl = null;

            if (userCreateInputModel.Avatar != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    userCreateInputModel.Avatar,
                    userCreateInputModel.UserName,
                    avatarFolder);
            }

            UserCreateServiceModel userCreateServiceModel = new UserCreateServiceModel { 
                FirstName = userCreateInputModel.FirstName,
                LastName = userCreateInputModel.LastName,
                UserName = userCreateInputModel.UserName,
                Email = userCreateInputModel.Email,
                PhoneNumber = userCreateInputModel.PhoneNumber,
                BirthDay = DateTime.ParseExact(userCreateInputModel.BirthDay, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Gender = userCreateInputModel.Gender,
                Password = userCreateInputModel.Password,
                Avatar = pictureUrl
            };


            await this.usersService.Create(userCreateServiceModel);

            return this.Redirect("/admin/users/list");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var isUserDeleted = await userManager.DeleteAsync(user);

            if (isUserDeleted != null)
            {
                return this.StatusCode(200);
            }

            return this.StatusCode(400);
        }

    }
}