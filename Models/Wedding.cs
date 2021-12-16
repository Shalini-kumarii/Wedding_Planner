using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wedding_Planner.Extensions;

namespace Wedding_Planner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Field must be 3 characters or more")]
        [Display(Name = "Wedder one:")]
        public string WedderOne { get; set; }


        [Required]
        [MinLength(3, ErrorMessage = "Field must be 3 characters or more")]
        [Display(Name = "Wedder Two:")]
        public string WedderTwo { get; set; }

        
        [Required]
        [FutureDate(ErrorMessage="Date should be in the future.")]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Field must be 3 characters or more")]
        [Display(Name = "Wedding Address:")]
        public string Address { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Foreign key and nevigation property
        
        public int UserId { get; set; }
        public User LoggedinUser { get; set; }

        // This property is exclusively for our form
      //  public List<User> AvailableUsers { get; set; }
      public List<RSVP> WeddingGuest { get; set; }


    }
}