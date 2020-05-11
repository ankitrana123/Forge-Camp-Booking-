using BusinessLayer.ServiceModels;
using DataAccess.DataAccessModel;
using DataAccess.DataAccessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceOperations
{
    public class CampingOperations
    {
        //adds a new camp
        public Guid AddNewCamp(CampModel camp)
        {
            Camp Newcamp = new Camp()
            {
                Image = camp.Image,

                Id = Guid.NewGuid(),

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,
            };
            CampDataAccess campDataAcces = new CampDataAccess();
            return campDataAcces.AddNewCamp(Newcamp);

        }

        //return the list of Camps with edit update buttons
        public List<CampModel> GetAllCamps()
        {
            CampDataAccess campDataAcces = new CampDataAccess();
            var allCamps = campDataAcces.GetAllCamps().Select(camp => new CampModel()
            {

                Image = camp.Image,

                Id = camp.Id,

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,

                Rating = camp.Rating ?? default(int)

            }).ToList();

            return allCamps;
        }

        //get camp details using its campid
        public Camp GetCamp(Guid campId)
        {
            CampDataAccess campDataAcces = new CampDataAccess();

            var requiredCamp = campDataAcces.GetCamp(campId);
            var campModel = new CampModel()
            {

                Image = requiredCamp.Image,

                Id = requiredCamp.Id,

                IsBooked = requiredCamp.IsBooked,

                Title = requiredCamp.Title,

                Amount = requiredCamp.Amount,

                Capacity = requiredCamp.Capacity,

                Description = requiredCamp.Description,

            };

            return requiredCamp;

        }

        //updates a camp details in db
        public void UpdateCamp(CampModel camp)
        {
            Camp newCamp = new Camp()
            {
                Image = camp.Image,

                Id = camp.Id,

                IsBooked = camp.IsBooked,

                Title = camp.Title,

                Amount = camp.Amount,

                Capacity = camp.Capacity,

                Description = camp.Description,
            };
            CampDataAccess campDataAcces = new CampDataAccess();
            campDataAcces.UpdateCamp(newCamp);

        }

        //deletes a camp using its campId
        public Camp DeleteCamp(Guid campId)
        {
            CampDataAccess campDataAcces = new CampDataAccess();
            var isCampDeleted = campDataAcces.DeleteCamp(campId);
            return isCampDeleted;
        }

        //return all filtered camps between checkin and checkout and with given capacity
        public List<CampModel> getFilteredCamps(DateTime checkInDate , DateTime checkOutDate,int capacity)
        {
            BookingDataAccess bookingDataServices = new BookingDataAccess();
            CampDataAccess campDataServices = new CampDataAccess();
            var bookedCamps = bookingDataServices.campsBetween(checkInDate, checkOutDate);
            var nonBookedCamps = campDataServices.getFilteredCamps(bookedCamps, capacity);

            return nonBookedCamps.Select(camp => DataAccessToService(camp)).ToList();




        }

        //Mapping from dataaccess to service layer
        private CampModel DataAccessToService(Camp requiredCamp)
        {
            var campModel = new CampModel()
            {

                Image = requiredCamp.Image,

                Id = requiredCamp.Id,

                IsBooked = requiredCamp.IsBooked,

                Title = requiredCamp.Title,

                Amount = requiredCamp.Amount,

                Capacity = requiredCamp.Capacity,

                Description = requiredCamp.Description,

                Rating = requiredCamp.Rating?? default(int)

            };
            return campModel;
        }

        public void GetRating(string referenceNumber, int rating)
        {
            BookingDataAccess bookingDataAccess = new BookingDataAccess();
            bookingDataAccess.AddRating(referenceNumber, rating);
           
        }
    }
}
