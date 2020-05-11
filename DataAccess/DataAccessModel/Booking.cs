//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.DataAccessModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public string ReferenceNumber { get; set; }
        public int TotalNights { get; set; }
        public string BillingAddress { get; set; }
        public long CellPhone { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public System.DateTime CheckInDate { get; set; }
        public System.DateTime CheckOutDate { get; set; }
        public System.Guid CampId { get; set; }
        public System.Guid UserId { get; set; }
        public int Capacity { get; set; }
        public Nullable<int> Rating { get; set; }
    
        public virtual Camp Camp { get; set; }
        public virtual User User { get; set; }
    }
}
