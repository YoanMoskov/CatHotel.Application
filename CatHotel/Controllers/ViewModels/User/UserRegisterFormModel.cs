namespace CatHotel.Controllers.ViewModels.User
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Models.User;

    using static Data.DataConstants.User;
    using static Data.ErrorMessageConstants.ClientErrorMessages;

    public class UserRegisterFormModel
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
}