using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wedding_Planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;  //This is where session comes from
namespace Wedding_Planner.Controllers     //be sure to use your own project's namespace!
{
    public class HomeController : Controller   //remember inheritance??
    {

        private MyContext _context;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("")]      // Both lines can be written in one line
        public ViewResult Index()
        {



            return View("Index");
        }
        [HttpPost("register")]
        public IActionResult Register(User newuser)
        {
            // Check initial ModelState
            if (ModelState.IsValid)
            {
                // If a User exists with provided email
                if (_context.Users.Any(u => u.Email == newuser.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");

                    // You may consider returning to the View at this point
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newuser.Password = Hasher.HashPassword(newuser, newuser.Password);
                    _context.Add(newuser);
                    _context.SaveChanges();
                    return RedirectToAction("LoginPageRander");


                }
            }
            else
            {
                return View("Index");
            }

        }


        [HttpGet("LoginPageRander")]

        public ViewResult LoginPageRander()
        {
            // return View("Login");
            return View("Index");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                // If no user exists with provided email
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    // handle failure (this should be similar to how "existing email" is handled)
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }

                else
                {
                    System.Console.WriteLine("Login Passed");
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                // ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");
            }

        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }

            // User LoggedInUser = _context.Users.FirstOrDefault(user => user.UserId == UserId);
            DashboardViews ViewModel = new DashboardViews()
            {
                // LoggedInUser= LoggedInUser,
                LoggedInUser = _context.Users.FirstOrDefault(u => u.UserId == (int)UserId),
                ToGetweddingList = _context.Weddings
                                    .Include(m => m.WeddingGuest)
                                    .ThenInclude(t => t.AttendingUser)
                                    .ToList()
            };
            return View("Dashboard", ViewModel);
        }


        [HttpGet("NewWedding")]

        public IActionResult CreateWedding()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }

            User LoggedInUser = _context.Users.FirstOrDefault(user => user.UserId == UserId);

            Wedding loggeduser = new Wedding();
            loggeduser.LoggedinUser = LoggedInUser;
            return View("NewWedding", loggeduser);
        }


        [HttpPost("NewWedding")]
        public IActionResult NewWedding(Wedding fromform)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("UserId", (int)fromform.UserId);
                _context.Add(fromform);
                _context.SaveChanges();
                return RedirectToAction("ShowWedding", fromform.WeddingId);
            }
            else
            {
                return CreateWedding();
            }
        }

        [HttpPost("DoRSVP")]

        public IActionResult DoRSVP(int weddingid, DashboardViews ViewModel)

        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                RSVP fromForm = new RSVP();
                fromForm.WeddingId = ViewModel.rsvpfrom.WeddingId;
                fromForm.UserId = UserId.Value;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Dashboard");
            }
        }

        [HttpPost("DoUnRSVP")]

        public IActionResult DoUnRSVP(int weddingid, DashboardViews ViewModel)

        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                RSVP toDelete = _context.RSVPs.Where(plm => plm.UserId == UserId.Value)
                .FirstOrDefault(s => s.WeddingId == ViewModel.rsvpfrom.WeddingId);
                _context.Remove(toDelete);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Dashboard");
            }
        }

        [HttpPost("DoDelete")]

        public IActionResult DoDelete(int weddingid, DashboardViews ViewModel)

        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Wedding toDelete = _context.Weddings.Where(plm => plm.WeddingId == ViewModel.rsvpfrom.WeddingId).FirstOrDefault();
                _context.Remove(toDelete);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Dashboard");
            }
        }

        // [HttpGet("wedding/{WeddingId}")]
        // public IActionResult WeddingInfo(int WeddingId)
        // {
        //     Wedding showWedding = new Wedding();

        //     showWedding.
        //     return RedirectToAction("ShowWedding", Wedding);
        // }

        [HttpGet("wedding/{WeddingId}/ShowWedding")]
        public IActionResult ShowWedding(int WeddingId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // LoggedInUser = _context.Users.FirstOrDefault(u => u.UserId == (int)UserId),
                DashboardViews ViewModel = new DashboardViews()
                {
                    // LoggedInUser= LoggedInUser,
                    LoggedInUser = _context.Users.FirstOrDefault(u => u.UserId == (int)UserId),
                    RenderWedding = _context.Weddings
                                    .Where(m => m.WeddingId == WeddingId)
                                    .Include(i => i.WeddingGuest)
                                    .ThenInclude(t => t.AttendingUser)
                                    .FirstOrDefault()

                };
                return View("ShowWedding", ViewModel);
            }
        }
        [HttpGet("Logout")]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



    }

}