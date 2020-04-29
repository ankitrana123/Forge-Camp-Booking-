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

        [HttpGet]
        [Route("AllCampDetails")]
        public List<CampViewModel> GetCamps()
        {

            CampingOperations campingOperations = new CampingOperations();
            var allCamps = campingOperations.GetAllCamps().Select(camp => new CampViewModel()
            {

                Image = camp.Image,

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,

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

        [HttpGet]
        [Route("GetCampDetailsById/{campId}")]
        public IHttpActionResult GetCampById(Guid campId)
        {


            CampingOperations campingOperations = new CampingOperations();

            var camp = campingOperations.GetCamp(campId);
            var CampViewModel = new CampViewModel()
            {
                Image = camp.Image,

                //Id = camp.Id,

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

        //[Authorize(Roles = "Admin")]
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

                    //Id = camp.Id,

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

        //[Authorize(Roles = "Admin")]
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

        //[Authorize(Roles ="Admin")]
        [Route("DeleteCamp")]
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

        [Route("GetCampsBetween/{checkinDate}/{checkOutDate}/{capacity}")]
        public List<CampViewModel> GetCampsBetween(DateTime checkinDate, DateTime checkOutDate, int capacity)
        {
            BookingOperations bookingOperations = new BookingOperations();
            CampingOperations campingOperations = new CampingOperations();
            var availabeCamps = bookingOperations.GetCamps(checkinDate, checkOutDate, capacity);

            var requiredCamps = availabeCamps.Select(camp => new CampViewModel()
            {
                Image = camp.Image,

                Id = camp.Id,

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,
            }).ToList();

            try
            {
                return requiredCamps;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}
