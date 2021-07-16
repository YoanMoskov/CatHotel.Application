namespace CatHotel.Infrastructure
{
    using Models.Reservation.FormModels;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Arrival : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var res = (ReservationFormModel)validationContext.ObjectInstance;

            return res.Arrival < DateTime.UtcNow ? new ValidationResult(GetErrorMessage(res)) : ValidationResult.Success;
        }

        private string GetErrorMessage(ReservationFormModel res)
        {
            return $"The arrival should be after {DateTime.UtcNow:d}.";
        }
    }

    public class Departure : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value,
            ValidationContext validationContext)
        {
            var res = (ReservationFormModel)validationContext.ObjectInstance;

            return res.Departure < res.Arrival ? new ValidationResult(GetErrorMessage(res)) : ValidationResult.Success;
        }

        private string GetErrorMessage(ReservationFormModel res)
        {
            var date = DateTime.UtcNow;
            if (!(res.Arrival < DateTime.UtcNow))
            {
                date = res.Arrival;
            }
            return $"The departure should be after {date:d}.";
        }
    }

    public class CatIds : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var res = (ReservationFormModel) validationContext.ObjectInstance;

            return res.CatIds == null ? new ValidationResult(GetErrorMessage(res)) : ValidationResult.Success;
        }

        private string GetErrorMessage(ReservationFormModel res)
        {
            return "You should choose at least one cat.";
        }
    }
}