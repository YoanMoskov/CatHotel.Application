namespace CatHotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Address;

    public class Address
    {
        [Key]
        public string AddressId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(FullAddressMaxLength)]
        public string FullAddress { get; set; }
    }
}