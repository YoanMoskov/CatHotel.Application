namespace CatHotel.Models.User
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using static Data.DataConstants.User;
    using static Data.ErrorMessageConstants.ClientErrorMessages;

    public class UserRegisterFormModel
    {
        [DisplayName("First name")]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
        public string FirstName { get; init; }

        [DisplayName("Last name")]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
        public string LastName { get; init; }

        [StringLength(MaxUsernameLength, MinimumLength = MinUsernameLength, ErrorMessage = UsernameError)]
        public string Username { get; init; }

        [DataType(DataType.Password)]
        public string Password { get; init; }

        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; init; }

        [DisplayName("Date of birth")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime BirthDate { get; init; }

        [RegularExpression(EmailRegex, ErrorMessage = EmailError)]
        public string Email { get; init; }

        [DisplayName("Phone number")]
        [StringLength(MaxPhoneNumberLength, MinimumLength = MinPhoneNumberLength, ErrorMessage = PhoneNumberError)]
        public string PhoneNumber { get; init; }
    }
}