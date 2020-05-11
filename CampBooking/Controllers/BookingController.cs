using BusinessLayer.ServiceModels;
using BusinessLayer.ServiceOperations;
using CampBooking.Models;
using DataAccess.DataAccessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CampBooking.Controllers
{
    [RoutePrefix("Api/Booking")]
    public class BookingController : ApiController
    {

        /// <summary>
        /// Creates a new booking the given camp
        /// </summary>
        /// <param name="book">details of the booking to be created</param>
        /// <returns>randomly generated 8 digit booking ref. number</returns>
        [HttpPost]
        [Route("CreateBooking")]
        public string PostCreateBooking(BookingViewModel book)
        {

            try
            {
                BookingOperations bookingOperations = new BookingOperations();
                BookingModel bookingModel = new BookingModel()
                {
                    BillingAddress = book.BillingAddress,

                    Capacity = book.Capacity,

                    CellPhone = book.CellPhone,

                    CheckInDate=book.CheckInDate,

                    CheckOutDate = book.CheckOutDate,

                    Country = book.Country,

                    TotalNights = book.TotalNights,


                    State = book.State,

                    ZipCode = book.ZipCode,

                    UserId = book.UserId,

                    CampId = book.CampId,
                    
                    
                };
                return  bookingOperations.CreateBooking(bookingModel);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        /// <summary>
        /// returns details of a booking using its reference number
        /// </summary>
        
        [HttpGet]
        [Route("GetBookingDetails/{bookingReferenceNumber}")]
        public BookingViewModel GetBookingDetails(string bookingReferenceNumber)
        {
            try
            {
                BookingOperations bookingOperations = new BookingOperations();
                var RequiredBooking = bookingOperations.fetchBooking(bookingReferenceNumber);
                BookingViewModel bookingViewModel = new BookingViewModel()
                {
                    BillingAddress = RequiredBooking.BillingAddress,

                    Capacity = RequiredBooking.Capacity,

                    CellPhone = RequiredBooking.CellPhone,

                    CheckInDate = RequiredBooking.CheckInDate,

                    CheckOutDate = RequiredBooking.CheckOutDate,

                    Country = RequiredBooking.Country,

                    TotalNights = RequiredBooking.TotalNights,

                    ReferenceNumber = RequiredBooking.ReferenceNumber,

                    State = RequiredBooking.State,

                    ZipCode = RequiredBooking.ZipCode,

                    UserId = RequiredBooking.UserId,

                    CampId = RequiredBooking.CampId,
                };
                return bookingViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a booking using its booking reference number
        /// </summary>
        
        [HttpDelete]
        [Route("CancelBooking/{bookingReferenceNumber}")]
        public IHttpActionResult DeleteCancelBooking(string bookingReferenceNumber)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BookingOperations bookingOperations = new BookingOperations();
                
                bool IsBookingCanceled = bookingOperations.CancelBooking( bookingReferenceNumber);
                if (!IsBookingCanceled)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

       
        

       
    }
}
