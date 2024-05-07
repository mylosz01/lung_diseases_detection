using System.ComponentModel.DataAnnotations;

namespace LungMed.Validators
{
    public class DateBeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                ErrorMessage = "Date must be before today";
                return date < DateTime.Now;
            }
            return false;
        }
    }
}
