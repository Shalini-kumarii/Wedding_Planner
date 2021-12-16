// using System.ComponentModel.DataAnnotations;
// namespace BeltPrep.Extensions
// {
//     public class OnlyHalfNumbersAttribute : ValidationAttribute
//     {
//         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//         {
//             double valueGiven = (double)value;

//             if(valueGiven % 0.5 != 0)
//             {
//                 return new ValidationResult("Must be in increments of 0.5");
//             }
//             return ValidationResult.Success;
//         }
//     }
// }
using System;
using System.ComponentModel.DataAnnotations;

namespace Wedding_Planner.Extensions
{
    public class FutureDateAttribute : ValidationAttribute
    {
       
            public override bool IsValid(object value)
    {
        return value != null && (DateTime)value > DateTime.Now;
    }
    }
}
