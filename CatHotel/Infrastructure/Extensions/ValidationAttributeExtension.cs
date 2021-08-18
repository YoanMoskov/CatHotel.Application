namespace CatHotel.Infrastructure.Extensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Models.Grooming.FormModel;
    using Models.Reservation.FormModels;

    public class Arrival : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var res = (ResFormModel) validationContext.ObjectInstance;

            return res.Arrival < Convert.ToDateTime($"{DateTime.UtcNow:d}")
                ? new ValidationResult($"The arrival should be {DateTime.UtcNow:d} or later.")
                : ValidationResult.Success;
        }
    }

    public class Departure : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var res = (ResFormModel) validationContext.ObjectInstance;

            var date = DateTime.UtcNow;
            if (!(res.Arrival < DateTime.UtcNow)) date = res.Arrival;

            return res.Departure <= res.Arrival
                ? new ValidationResult($"The departure should be after {date:d}")
                : ValidationResult.Success;
        }
    }

    public class CatIds : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var res = (ResFormModel) validationContext.ObjectInstance;

            return res.CatIds == null
                ? new ValidationResult("You should choose at least one cat.")
                : ValidationResult.Success;
        }
    }

    public class Appointment : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var grooming = (AddGroomingModel) validationContext.ObjectInstance;

            return grooming.Appointment <= DateTime.UtcNow
                ? new ValidationResult($"The appointment should be after {DateTime.UtcNow:d} or later.")
                : ValidationResult.Success;
        }
    }
}