using System;
using System.ComponentModel.DataAnnotations;

namespace Helsi.Dto.Attributes
{
    public class CustomDateRangeAttribute : ValidationAttribute
    {
        private DateTime MinDate { get; set; }
        private DateTime MaxDate { get; set; }

        public CustomDateRangeAttribute(string minDate)
        {
            MinDate = Convert.ToDateTime(minDate);
            MaxDate = DateTime.Today;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (date >= MinDate && date <= MaxDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Date must be since {MinDate.ToString()} till {MaxDate.ToString()}");
            }
        }
    }
}
