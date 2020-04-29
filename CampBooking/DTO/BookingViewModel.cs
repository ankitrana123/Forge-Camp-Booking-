using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampBooking.Models
{
    public class BookingViewModel
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

        public int Capacity { get; set; }

        public System.Guid CampId { get; set; }

        public System.Guid UserId { get; set; }
    }
}