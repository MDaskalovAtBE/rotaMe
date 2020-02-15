using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Web.ViewModels.Identity.Register;
using RotaMe.Services.Mapping;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace RotaMe.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<RotaMeUser> _signInManager;
        private readonly UserManager<RotaMeUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRegisterService _registerService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUsersService usersService;

        private readonly string _maleProfilePicture = "https://res.cloudinary.com/rotame/image/upload/v1578736049/profile-pictures/male-profile-picture_kaausb.png";
        private readonly string _femaleProfilePicture = "https://res.cloudinary.com/rotame/image/upload/v1578735954/profile-pictures/female-profile-picture_okn8ca.png";
        private readonly string avatarFolder = "profile-pictures";

        public RegisterModel(
            UserManager<RotaMeUser> userManager,
            SignInManager<RotaMeUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRegisterService registerService,
            ICloudinaryService cloudinaryService,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _registerService = registerService;
            this.cloudinaryService = cloudinaryService;
            this.usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [MinLength(3)]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [MinLength(3)]
            [Display(Name = "Firstname")]
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [Required]
            [Display(Name = "BirthDay")]
            public string BirthDay { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            public IFormFile Avatar { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            var allGenders = this._registerService.GetAllGenders();

            this.ViewData["genders"] = await allGenders.To<RegisterGenderViewModel>().ToListAsync();

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var isRoot = !_userManager.Users.Any();
                var gender = _registerService.GetGender(Input.Gender); 
                
                string avatar = null;

                if (Input.Avatar != null)
                {
                    avatar = await this.cloudinaryService.UploadPictureAsync(
                        Input.Avatar,
                        Input.UserName,
                        avatarFolder);
                }

                if (avatar == null)
                {
                    avatar = (Input.Gender == "Male") ? _maleProfilePicture : _femaleProfilePicture;
                }
                
                var user = new RotaMeUser 
                { 
                    UserName = Input.UserName, 
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    BirthDay = DateTime.ParseExact(Input.BirthDay, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Gender = gender,
                    Avatar = avatar,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "User");
                    if (isRoot)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        await usersService.SetLastLoggedIn(Input.UserName, DateTime.UtcNow);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            var allGenders = this._registerService.GetAllGenders();

            this.ViewData["genders"] = await allGenders.To<RegisterGenderViewModel>().ToListAsync();
            return Page();
        }
    }
}
