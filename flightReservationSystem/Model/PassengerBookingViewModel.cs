using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace flightReservationSystem.Model
{
    public class PassengerBookingViewModel
    {
        // User Information
        public int UserId { get; set; }

        // Flight Information
        public int FlightId { get; set; }

        // Booking Information
        public int NumPassengers { get; set; }
        public decimal TotalAmount { get; set; }

        // List of Passengers
        public List<PassengerType> Passengers { get; set; }
    }

    public class PassengerType
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string PassportNumber { get; set; }
    }
}