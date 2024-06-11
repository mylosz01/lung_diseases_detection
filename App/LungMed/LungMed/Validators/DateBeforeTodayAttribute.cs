using System;
using System.ComponentModel.DataAnnotations;

namespace LungMed.Validators
{
    public class DateBeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime < DateTime.Now;
            }
            return true; // or false depending on your requirements
        }
    }
}
