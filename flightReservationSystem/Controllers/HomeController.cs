using flightReservationSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace flightReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        BookingSystemEntities db = new BookingSystemEntities();
        public ActionResult Index()
        {
            ViewBag.cities = db.Cities.OrderBy(x => x.CityName).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            try
            {
                db.Database.ExecuteSqlCommand("[dbo].[AddUser] @UserName, @Password, @Email, @FirstName, @LastName",
                    new SqlParameter("UserName", user.Username),
                    new SqlParameter("Password", user.Password),
                    new SqlParameter("Email", user.Email),
                    new SqlParameter("FirstName", user.FirstName),
                    new SqlParameter("LastName", user.LastName)
                    
                );

                return RedirectToAction("UserLogin", "Home");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult SearchFlight(Flight model)
        {
            try
            {
                List<Flight> results = db.Database.SqlQuery<Flight>(
                    "[dbo].[SearchFlights] @DepartureCityID, @ArrivalCityID, @DepartureDate",
                    new SqlParameter("DepartureCityID", model.DepartureCityID),
                    new SqlParameter("ArrivalCityID", model.ArrivalCityID),
                    new SqlParameter("DepartureDate", model.DepartureDateTime.ToString())
                ).ToList();

                

                return View(results); 
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public ActionResult UserLogin()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(User user)
        {
            var bilgiler = db.Users.Where(x => x.Username == user.Username.Trim() &&
                x.Password == user.Password).FirstOrDefault();

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Username, false);
                Session["Username"] = bilgiler.Username;
                Session["UserId"] = bilgiler.UserID;
                return RedirectToAction("Index", "Home");
            }

            return View();

           
        }
        public ActionResult MakeBooking(int userid,int flightId)
        {
            ViewBag.UserId = userid;
            ViewBag.FlightId = flightId;

            return View();
        }
        [HttpPost]
        public ActionResult MakeBooking(PassengerBookingViewModel bookingViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(bookingViewModel);
                }

                //var passengersDataTable = CreatePassengersDataTable(bookingViewModel.Passengers);

                db.Database.ExecuteSqlCommand("EXEC MakeBooking @UserID, @FlightID, @NumPassengers, @TotalAmount, @PassengersInfo",
                    new SqlParameter("@UserID", bookingViewModel.UserId),
                    new SqlParameter("@FlightID", bookingViewModel.FlightId),
                    new SqlParameter("@NumPassengers", bookingViewModel.NumPassengers),
                    new SqlParameter("@TotalAmount", bookingViewModel.TotalAmount),
                   new SqlParameter("@PassengersInfo", CreatePassengersDataTable(bookingViewModel.Passengers)) { TypeName = "dbo.PassengerType" }

                );

                return RedirectToAction("GetUserBookings", "Home", new { userId = bookingViewModel.UserId });

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        private DataTable CreatePassengersDataTable(List<PassengerType> passengers)
        {
            DataTable dataTable = new DataTable("dbo.PassengerType");
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("DateOfBirth", typeof(DateTime));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("PassportNumber", typeof(string));

            foreach (var passenger in passengers)
            {
                dataTable.Rows.Add(
                    passenger.FirstName,
                    passenger.LastName,
                    passenger.DateOfBirth.ToString(),
                    passenger.Email,
                    passenger.PhoneNumber,
                    passenger.Gender,
                    passenger.PassportNumber
                );
            }

            return dataTable;
        }


        public ActionResult GetUserBookings(int userId)
        {
            try
            {
                var userBookings = db.Database.SqlQuery<UserBookingsViewModel>(
                    "EXEC GetUserBookings @UserID",
                    new SqlParameter("@UserID", userId)
                ).ToList();

                return View(userBookings);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                return View("Error");
            }
        }
        public ActionResult Contact()
        {
           

            return View();
        }
    }
}