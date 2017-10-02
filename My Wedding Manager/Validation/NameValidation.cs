using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace My_Wedding_Manager.Validation
{
    public class NameValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide name");
            }
            else if (value.ToString().Contains("@"))
            {
                return new ValidationResult("Name should not contain @");
            }
            return ValidationResult.Success;
        }
    }
}