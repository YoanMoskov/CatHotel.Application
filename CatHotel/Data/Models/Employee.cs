namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Employee;

    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(MaxEmailLength)]
        public string Email { get; set; }

        public bool IsVeterinarian { get; set; }

        public bool IsWorking { get; set; }

        public ICollection<Cat> Cats { get; set; } = new List<Cat>();
    }
}