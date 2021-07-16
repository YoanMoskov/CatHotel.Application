namespace CatHotel.Models.User.FormModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using static Data.DataConstants.User;
    using static Data.ErrorMessageConstants.ClientErrorMessages;

    public class UserRegisterFormModel
    {
        [Required]
        [DisplayName("First name")]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
        public string FirstName { get; init; }

        [Required]
        [DisplayName("Last name")]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = FullNameError)]
        public string LastName { get; init; }

        [Required]
        [StringLength(MaxUsernameLength, MinimumLength = MinUsernameLength, ErrorMessage = UsernameError)]
        public string Username { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; init; }

        [Required]
        [DisplayName("Date of birth")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime BirthDate { get; init; }

        [Required]
        [RegularExpression(EmailRegex, ErrorMessage = EmailError)]
        public string Email { get; init; }

        [Required]
        [DisplayName("Phone number")]
        [StringLength(MaxPhoneNumberLength, MinimumLength = MinPhoneNumberLength, ErrorMessage = PhoneNumberError)]
        public string PhoneNumber { get; init; }
    }
}