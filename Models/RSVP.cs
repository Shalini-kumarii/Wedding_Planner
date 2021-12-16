using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

         //Foreign key and nevigation property

        public int UserId { get; set; }
        public User AttendingUser { get; set; }

        public int WeddingId { get; set; }
        public Wedding AttendedWedding { get; set; }


    }
}