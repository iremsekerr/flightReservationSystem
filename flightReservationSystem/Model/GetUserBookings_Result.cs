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
    
    public partial class GetUserBookings_Result
    {
        public int BookingID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> FlightID { get; set; }
        public System.DateTime BookingDateTime { get; set; }
        public int NumPassengers { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
