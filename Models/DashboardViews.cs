using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wedding_Planner.Models
{
    public class DashboardViews
    {
        

        //public Category RenderCategory { get; set; }
        public User LoggedInUser { get; set; }
        public List<Wedding> ToGetweddingList { get; set; }
        public RSVP rsvpfrom { get; set; }

        public Wedding RenderWedding { get; set; }
    }
}