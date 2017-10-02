using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_Wedding_Manager.Validation
{
    public class ContactValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide contact number");
            }
            else if (value.ToString().Contains("@"))
            {
                return new ValidationResult("Contact should not contain @");
            }
            else if (value.ToString().Contains("#"))
            {
                return new ValidationResult("Contact should not contain #");
            }
            else if (value.ToString().Contains("*"))
            {
                return new ValidationResult("Contact should not contain *");
            }
            else if (value.ToString().Length > 11)
            {
                return new ValidationResult("Contact number should not be more than 11 digits");
            }
            return ValidationResult.Success;
        }
    }
}