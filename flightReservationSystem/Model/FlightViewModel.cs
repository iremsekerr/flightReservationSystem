using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flightReservationSystem.Model
{
    public class FlightViewModel
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int SeatCapacity { get; set; }
        public int? DepartureCityID { get; set; }
        public int? ArrivalCityID { get; set; }
        public decimal? PricePerSeat { get; set; }
        public decimal TotalRevenue { get; set; } // New property for total revenue

        public decimal AveragePassengerAge { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
    }
}