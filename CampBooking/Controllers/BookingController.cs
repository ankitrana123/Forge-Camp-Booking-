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

        //[Authorize(Roles = "User")]
        [HttpPost]
        [Route("CreateBooking")]
        public IHttpActionResult PostCreateBooking([FromBody]BookingViewModel book)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

                    //ReferenceNumber = book.ReferenceNumber,

                    State = book.State,

                    ZipCode = book.ZipCode,

                    UserId = book.UserId,

                    CampId = book.CampId,
                    
                    
                };
                bookingOperations.CreateBooking(bookingModel);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(book);
        }

        //[Authorize(Roles = "User")]
        [HttpGet]
        [Route("GetBookingDetails")]
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

        //[Authorize(Roles = "User")]
        [HttpDelete]
        [Route("CancelBooking")]
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
