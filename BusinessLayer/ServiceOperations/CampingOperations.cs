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

            }).ToList();

            return allCamps;
        }

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

        public Camp DeleteCamp(Guid campId)
        {
            CampDataAccess campDataAcces = new CampDataAccess();
            var isCampDeleted = campDataAcces.DeleteCamp(campId);
            return isCampDeleted;
        }
    }
}
