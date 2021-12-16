using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        public string LogEmail { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string LogPassword { get; set; }

    }

}