using Microsoft.EntityFrameworkCore;
namespace Wedding_Planner.Models
{ 
    // the MyContext class representing a session with our MySQL 
    // database allowing us to query for or save data
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
  
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set;}

         public DbSet<RSVP> RSVPs { get; set;}
    }
}
