using BusinessLayer.ServiceModels;
using BusinessLayer.ServiceOperations;
using CampBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CampBooking.Controllers
{
    [RoutePrefix("Api/Camp")]
    public class CampController : ApiController
    {
        /// <summary>
        /// Returns the list of all the camps
        /// </summary>
        
        [HttpGet]
        [Route("AllCampDetails")]
        public List<CampViewModel> GetCamps()
        {

            CampingOperations campingOperations = new CampingOperations();
            var allCamps = campingOperations.GetAllCamps().Select(camp => new CampViewModel()
            {
                Id = camp.Id,

                Image = camp.Image,

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,

                Rating = camp.Rating

            }).ToList();

            try
            {
                return allCamps;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// return the list of camp based on its campId
        /// </summary>
        /// <param name="campId"> campId refers to the particular camp we want to get details of</param>
        
        [HttpGet]
        [Route("GetCampDetailsById/{campId}")]
        public IHttpActionResult GetCampById(Guid campId)
        {


            CampingOperations campingOperations = new CampingOperations();

            var camp = campingOperations.GetCamp(campId);
            var CampViewModel = new CampViewModel()
            {
                Image = camp.Image,
                

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,
            };

            try
            {

                if (CampViewModel == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(CampViewModel);
        }

        /// <summary>
        /// Creates a new camp in the db
        /// </summary>
        /// <param name="camp">take in a viewModel having all the camp details we want to create</param>
        
        [Authorize]
        [HttpPost]
        [Route("CreateCamp")]
        public IHttpActionResult PostCreateCamp(CampViewModel camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                CampingOperations campingOperations = new CampingOperations();
                var campModel = new CampModel()
                {
                    Image = camp.Image,


                    IsBooked = camp.IsBooked,

                    Title = camp.Title,

                    Amount = camp.Amount,

                    Capacity = camp.Capacity,

                    Description = camp.Description,
                };

                var campId = campingOperations.AddNewCamp(campModel);
            }
            catch (Exception)
            {
                throw;
            }



            return Ok(camp);
        }

        /// <summary>
        /// Updates the details of a particular camp 
        /// </summary>
        /// <param name="camp">takes in the details of the camp to be updated inside in viewmodel</param>
       
        [Authorize]
        [HttpPut]
        [Route("UpdateCamp")]
        public IHttpActionResult PutUpdateCamp(CampViewModel camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CampingOperations campingOperations = new CampingOperations();
                var campModel = new CampModel()
                {
                    Image = camp.Image,

                    Id = camp.Id,

                    IsBooked = camp.IsBooked,

                    Title = camp.Title,

                    Amount = camp.Amount,

                    Capacity = camp.Capacity,

                    Description = camp.Description,
                };
                campingOperations.UpdateCamp(campModel);


            }
            catch (Exception)
            {
                throw;
            }
            return Ok(camp);
        }

        /// <summary>
        /// Deletes a camp taking its camp id from db
        /// </summary>
        
        [Authorize]
        [Route("DeleteCamp/{campId}")]
        public IHttpActionResult DeleteRemoveCamp(Guid campId)
        {
            CampingOperations campingOperations = new CampingOperations();
            var isCampDeleted = campingOperations.DeleteCamp(campId);
            if (isCampDeleted == null)
            {
                return NotFound();
            }


            return Ok(isCampDeleted);
        }

        /// <summary>
        /// finds the list of available camps for the list of params
        /// </summary>
        /// <param name="checkinDate">the check in date of the camp</param>
        /// <param name="checkOutDate">the check out date of the camp</param>
        /// <param name="capacity">capacity of the camp</param>
        /// <returns>list of available camps of the given params</returns>
        /// 
        [Route("GetCampsBetween/{checkinDate}/{checkOutDate}/{capacity}")]
        public List<CampModel> GetCampsBetween(DateTime checkinDate, DateTime checkOutDate, int capacity)
        {
            var today = DateTime.Now;
            var yesterdayDate = today.AddDays(-1);
            
            //return empty model is checkIndate>= checkOutDate
            if (!ModelState.IsValid || checkinDate < yesterdayDate || checkinDate > checkOutDate || checkOutDate == checkinDate)
            {

                List<CampModel> empty = new List<CampModel>();
                return empty;
            }

            BookingOperations bookingOperations = new BookingOperations();
            CampingOperations campingOperations = new CampingOperations();
            try
            {
                return campingOperations.getFilteredCamps(checkinDate, checkOutDate, capacity);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        /// <summary>
        ///1. Add rating for camp being booked
        /// 2. Find average rating for the booked camps and show on dashboard
        /// </summary>
        /// <param name="referenceNumber"></param> Booking reference number
        /// <param name="rating"></param> rating assigned to that camp
        [Route("GetRatingForBookedCamps/{referenceNumber}/{rating}")]
        public void GetRatingForBookedCamps(string referenceNumber, int rating)
        {
            CampingOperations campingOperations = new CampingOperations();
            campingOperations.GetRating(referenceNumber, rating);
        }
    }

}
