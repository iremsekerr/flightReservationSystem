//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace flightReservationSystem.Model
{
    using System;
    
    public partial class SearchFlights_Result
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public System.DateTime DepartureDateTime { get; set; }
        public System.DateTime ArrivalDateTime { get; set; }
        public int SeatCapacity { get; set; }
        public Nullable<int> DepartureCityID { get; set; }
        public Nullable<int> ArrivalCityID { get; set; }
    }
}
