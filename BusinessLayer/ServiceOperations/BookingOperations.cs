﻿using BusinessLayer.ServiceModels;
using DataAccess.DataAccessModel;
using DataAccess.DataAccessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceOperations
{
    public class BookingOperations
    {
        public string CreateBooking(BookingModel book)
        {
            Booking booking = new Booking()
            {


                TotalNights = book.TotalNights,

                BillingAddress = book.BillingAddress,

                CellPhone = book.CellPhone,

                State = book.State,

                Country = book.Country,

                ZipCode = book.ZipCode,

                CheckInDate = book.CheckInDate,

                CheckOutDate = book.CheckOutDate,

                Capacity = book.Capacity,

                UserId = book.UserId,

                CampId = book.CampId,

            };
            BookingDataAccess bookingDataAccess = new BookingDataAccess();
            return bookingDataAccess.CreateBooking(booking);
        }

        public Booking fetchBooking(string BookingReferenceNumber)
        {
            BookingDataAccess bookingDataAccess = new BookingDataAccess();
            return bookingDataAccess.fetchBooking(BookingReferenceNumber);

        }


        public bool CancelBooking(string BookingReferenceNumber)
        {
            BookingDataAccess bookingDataAccess = new BookingDataAccess();
            return bookingDataAccess.CancelBooking(BookingReferenceNumber);
        }

        public Booking fetchBookingForUser(Guid userId)
        {
            BookingDataAccess bookingDataAccess = new BookingDataAccess();
            return bookingDataAccess.fetchBookingForUser(userId);
        }



        
    }
}
