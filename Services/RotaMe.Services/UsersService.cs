using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using RotaMe.Sevices.Models.Administration.Roles;
using RotaMe.Sevices.Models.Administration.Users;
using RotaMe.Sevices.Models.Owner.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services
{
    public class UsersService : IUsersService
    {
        private readonly RotaMeDbContext context;
        private readonly SignInManager<RotaMeUser> signInManager;
        private readonly UserManager<RotaMeUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly IRegisterService registerService;

        private readonly string _maleProfilePicture = "https://res.cloudinary.com/rotame/image/upload/v1578736049/profile-pictures/male-profile-picture_kaausb.png";
        private readonly string _femaleProfilePicture = "https://res.cloudinary.com/rotame/image/upload/v1578735954/profile-pictures/female-profile-picture_okn8ca.png";

        public UsersService(RotaMeDbContext context,
            UserManager<RotaMeUser> userManager,
            SignInManager<RotaMeUser> signInManager,
            IEmailSender emailSender,
            IRegisterService registerService)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.registerService = registerService;
        }

        public IQueryable<ListUserServiceModel> GetAllUsers()
        {
            return this.context.Users.To<ListUserServiceModel>();
        }

        public IQueryable<ListUsersToAssignServiceModel> GetAllUsersToAssign()
        {
            return this.context.Users.To<ListUsersToAssignServiceModel>();
        }
        public async Task<IEnumerable<ListUsersToUnassignServiceModel>> GetAllUsersToUnassign()
        {
            var users = this.context.Users;
            var usersToUnassignServiceModel = await users.To<ListUsersToUnassignServiceModel>().ToListAsync();
            for (int i = 0; i < usersToUnassignServiceModel.Count(); i++)
            {
                var user = await userManager.FindByIdAsync(usersToUnassignServiceModel[i].Id);
                var userRoles = await userManager.GetRolesAsync(user);
                usersToUnassignServiceModel[i].Roles = userRoles;
            }
            return usersToUnassignServiceModel;
        }

        public async Task<UserDetailsServiceModel> GetUserById(string userId)
        {
            var user = await this.context.Users.To<UserDetailsServiceModel>().SingleOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user;
        }

        public async Task<bool> Create(UserCreateServiceModel userCreateServiceModel)
        {
            var gender = registerService.GetGender(userCreateServiceModel.Gender);
            var avatar = userCreateServiceModel.Avatar != null ? userCreateServiceModel.Avatar : (userCreateServiceModel.Gender == "Male") ? _maleProfilePicture : _femaleProfilePicture;

            var user = new RotaMeUser
            {
                UserName = userCreateServiceModel.UserName,
                Email = userCreateServiceModel.Email,
                FirstName = userCreateServiceModel.FirstName,
                LastName = userCreateServiceModel.LastName,
                PhoneNumber = userCreateServiceModel.PhoneNumber,
                BirthDay = userCreateServiceModel.BirthDay,
                Gender = gender,
                Avatar = avatar,
                
            };
            var result = await userManager.CreateAsync(user, userCreateServiceModel.Password);

            if (result.Succeeded)
            {

                await userManager.AddToRoleAsync(user, "User");


                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            }

            return result.Succeeded;
        }

        public async Task<bool> Edit(UserEditServiceModel userEditServiceModel)
        {
            var user = await userManager.FindByIdAsync(userEditServiceModel.Id);

            if (userEditServiceModel.Email != null)
            {
                user.Email = userEditServiceModel.Email;
                user.PhoneNumber = userEditServiceModel.PhoneNumber;
            }
            else if (userEditServiceModel.FirstName != null)
            {
                user.FirstName = userEditServiceModel.FirstName;
                user.LastName = userEditServiceModel.LastName;
                user.UserName = userEditServiceModel.UserName;
                user.BirthDay = DateTime.ParseExact(userEditServiceModel.BirthDay, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                user.Gender = registerService.GetGender(userEditServiceModel.Gender);
            }
            if (userEditServiceModel.Avatar != null)
            {
                user.Avatar = userEditServiceModel.Avatar;
            }

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task SetLastLoggedIn(string userName, DateTime date)
        {
            var user = await userManager.FindByNameAsync(userName);

            user.LastLoggedIn = date;

            await userManager.UpdateAsync(user);
        }

        public IQueryable<UsersListToAddToProjectServiceModel> GetAllUsersToAdd()
        {
            return this.context.Users.To<UsersListToAddToProjectServiceModel>();
        }
    }
}
