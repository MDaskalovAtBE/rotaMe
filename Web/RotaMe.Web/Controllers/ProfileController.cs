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
using RotaMe.Sevices.Models.Administration.Users;
using RotaMe.Web.InputModels.Administration.Users;
using RotaMe.Web.ViewModels.Administration.Users;
using RotaMe.Web.ViewModels.Identity.Register;

namespace RotaMe.Web.Controllers
{
    public class ProfileController : AuthorizeController
    {

        private readonly IUsersService usersService;
        private readonly UserManager<RotaMeUser> userManager;
        private readonly IRegisterService registerService;
        private readonly ICloudinaryService cloudinaryService; 
        private readonly string avatarFolder = "profile-pictures";

        public ProfileController(IUsersService usersService, UserManager<RotaMeUser> userManager, IRegisterService registerService, ICloudinaryService cloudinaryService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.registerService = registerService;
            this.cloudinaryService = cloudinaryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.User.FindFirst("Id").Value;

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
        public async Task<IActionResult> Index(UserEditInputModel userEditInputModel)
        {
            var id = HttpContext.User.FindFirst("Id").Value;

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

            return this.RedirectToAction("Index");
        }
    }
}