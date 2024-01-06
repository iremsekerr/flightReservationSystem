using flightReservationSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace flightReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        BookingSystemEntities db = new BookingSystemEntities();


        public ActionResult GetFlightList()
        {
            var flights = db.Flights.ToList();
            var flightViewModels = new List<FlightViewModel>();

            foreach (var flight in flights)
            {
                var totalRevenue = db.Database.SqlQuery<decimal>(
                    "SELECT dbo.GetTotalRevenueForFlight(@FlightID) AS TotalRevenue",
                    new SqlParameter("@FlightID", flight.FlightID)
                ).FirstOrDefault();

                var averagePassengerAge = db.Database.SqlQuery<decimal>(
                "SELECT ISNULL(dbo.GetAveragePassengerAgeForFlight(@FlightID), 0) AS AveragePassengerAge",
                new SqlParameter("@FlightID", flight.FlightID)
            ).FirstOrDefault();
                var flightViewModel = new FlightViewModel
                {
                    FlightID = flight.FlightID,
                    FlightNumber = flight.FlightNumber,
                    DepartureCity = flight.City.CityName,
                    ArrivalCity = flight.City1.CityName,
                    DepartureDateTime = flight.DepartureDateTime,
                    ArrivalDateTime = flight.ArrivalDateTime,
                    SeatCapacity = flight.SeatCapacity,
                    DepartureCityID = flight.DepartureCityID,
                    ArrivalCityID = flight.ArrivalCityID,
                    PricePerSeat = flight.PricePerSeat,
                    TotalRevenue = totalRevenue,
                    AveragePassengerAge= averagePassengerAge

                };

                flightViewModels.Add(flightViewModel);
            }

            return View(flightViewModels);
        }


        [HttpGet]
        public ActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFlight(Flight flight)
        {
            try
            {
                db.Database.ExecuteSqlCommand("[dbo].[AddFlight] @FlightNumber, @DepartureCityID, @ArrivalCityID, @DepartureDateTime, @ArrivalDateTime, @SeatCapacity, @PricePerSeat",
                    new SqlParameter("FlightNumber", flight.FlightNumber),
                    new SqlParameter("DepartureCityID", flight.DepartureCityID),
                    new SqlParameter("ArrivalCityID", flight.ArrivalCityID),
                    new SqlParameter("DepartureDateTime", flight.DepartureDateTime),
                    new SqlParameter("ArrivalDateTime", flight.ArrivalDateTime),
                    new SqlParameter("SeatCapacity", flight.SeatCapacity),
                    new SqlParameter("PricePerSeat", flight.PricePerSeat)
                );

                return RedirectToAction("GetFlightList", "Admin");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult RemoveFlight(int id)
        {
            var flight = db.Flights.Find(id);

            if (flight != null)
            {
                db.Flights.Remove(flight);
                db.SaveChanges();
            }

            return RedirectToAction("GetFlightList");
        }
     
        [HttpGet]
        public ActionResult UpdateFlight(int id)
        {
            ViewBag.cities = db.Cities.OrderBy(x => x.CityName).ToList();
            var flightToUpdate = db.Flights.Find(id);
            if (flightToUpdate == null)
            {
                return HttpNotFound();
            }

            return View(flightToUpdate);
        }

        [HttpPost]
        public ActionResult UpdateFlight(Flight c)
        {
            
            var flightToUpdate = db.Flights.Find(c.FlightID);
            if (flightToUpdate == null)
            {
                return HttpNotFound();
            }

            flightToUpdate.ArrivalCityID = c.ArrivalCityID;
            flightToUpdate.ArrivalDateTime = c.ArrivalDateTime;
            flightToUpdate.DepartureCityID = c.DepartureCityID;
            flightToUpdate.DepartureDateTime = c.DepartureDateTime;
            flightToUpdate.SeatCapacity = c.SeatCapacity;
            flightToUpdate.PricePerSeat = c.PricePerSeat;
            db.SaveChanges();
            return RedirectToAction("GetFlightList");
        }
        public ActionResult GetUserList()
        {
            var users = db.Users.ToList();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var totalBookings = db.Database.SqlQuery<int>(
                    "SELECT dbo.GetTotalBookingsForUser(@UserID) AS TotalBookings",
                    new SqlParameter("@UserID", user.UserID)
                ).FirstOrDefault();

                var userViewModel = new UserViewModel
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TotalBooked = totalBookings
                };

                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public ActionResult GetBookingList()
        {
            var bookings = db.Bookings.ToList();
            return View(bookings);
        }
    }
}