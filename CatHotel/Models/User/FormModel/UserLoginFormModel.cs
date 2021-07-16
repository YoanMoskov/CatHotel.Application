namespace CatHotel.Models.User.FormModel
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginFormModel
    {
        public string Username { get; init; }

        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; init; }
    }
}