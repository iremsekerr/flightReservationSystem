using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flightReservationSystem.Model
{
    public class UserBookingsViewModel
    {
        public int BookingID { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalCityName { get; set; }
        public DateTime FlightDepartureDateTime { get; set; }
        public DateTime FlightArrivalDateTime { get; set; }
        public int FlightSeatCapacity { get; set; }
        public decimal FlightPricePerSeat { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string PassportNumber { get; set; }
    }
}