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
    using System.Collections.Generic;
    
    public partial class Admin
    {
        public Admin()
        {
            this.Passenger = new HashSet<Passenger>();
        }
    
        public string Aname { get; set; }
        public string Apass { get; set; }
    
        public virtual ICollection<Passenger> Passenger { get; set; }
    }
}
