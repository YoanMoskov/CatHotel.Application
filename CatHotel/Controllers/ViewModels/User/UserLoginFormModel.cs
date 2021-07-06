namespace CatHotel.Controllers.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginFormModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}