namespace CatHotel.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Controllers.Models.User;
    using Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static Data.DataConstants.User;
    using static Data.ErrorMessageConstants.ClientErrorMessages;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [DisplayName("First name")]
            [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
            public string FirstName { get; set; }

            [DisplayName("Last name")]
            [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
            public string LastName { get; set; }

            [StringLength(MaxUsernameLength, MinimumLength = MinUsernameLength, ErrorMessage = UsernameError)]
            public string Username { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DisplayName("Confirm password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }

            [DisplayName("Date of birth")]
            [BindProperty, DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            [RegularExpression(EmailRegex, ErrorMessage = EmailError)]
            public string Email { get; set; }

            [DisplayName("Phone number")]
            [StringLength(MaxPhoneNumberLength, MinimumLength = MinPhoneNumberLength, ErrorMessage = PhoneNumberError)]
            public string PhoneNumber { get; set; }

            public AddressFormModel AddressFormViewModel { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Username,
                    BirthDate = Input.BirthDate,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    Address = new Address()
                    {
                        Country = Input.AddressFormViewModel.Country,
                        City = Input.AddressFormViewModel.City,
                        FullAddress = Input.AddressFormViewModel.FullAddress
                    }
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
