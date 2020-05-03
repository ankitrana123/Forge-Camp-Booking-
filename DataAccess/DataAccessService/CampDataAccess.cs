using DataAccess.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.DataAccessService
{
    public class CampDataAccess
    {

        //only allowed for the admin to add new camp
        public Guid AddNewCamp(Camp camp)
        {
            using (var context = new CampDBEntities())
            {
                context.Camps.Add(camp);

                context.SaveChanges();

                return camp.Id;
            }

        }

        //return the list of Camps with edit update buttons
        public List<Camp> GetAllCamps()
        {
            using (var context = new CampDBEntities())
            {
                var requiredCamps = context.Camps.ToList();
                return requiredCamps;
            }
        }

        public Camp GetCamp(Guid campId)
        {
            using (var context = new CampDBEntities())
            {

                var requiredCamp = context.Camps.Where(s => s.Id == campId).FirstOrDefault();
                return requiredCamp;

            }
        }

        public void UpdateCamp(Camp camp)
        {
            using (var context = new CampDBEntities())
            {
                var requiredCamp = GetCamp(camp.Id);

                requiredCamp.Image = camp.Image;

                requiredCamp.Id = camp.Id;

                requiredCamp.IsBooked = camp.IsBooked;

                requiredCamp.Title = camp.Title;

                requiredCamp.Amount = camp.Amount;

                requiredCamp.Capacity = camp.Capacity;

                requiredCamp.Description = camp.Description;
                try
                {
                    if (context.Entry(requiredCamp).State == System.Data.Entity.EntityState.Detached)
                    {
                        context.Camps.Attach(requiredCamp);
                    }
                    else
                    {
                        context.Entry(requiredCamp).CurrentValues.SetValues(camp);
                    }
                    context.Entry(requiredCamp).State = System.Data.Entity.EntityState.Modified;
                    var resultSet = context.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        //[Authorize(Roles = "Admin")]
        public Camp DeleteCamp(Guid campId)
        {
            using (var context = new CampDBEntities())
            {

                var requiredCamp = GetCamp(campId);
                BookingDataAccess bookingDataAccess = new BookingDataAccess();
                
                if (requiredCamp != null)
                {
                    try
                    {
                        if (context.Entry(requiredCamp).State == System.Data.Entity.EntityState.Detached)
                        {
                            bookingDataAccess.removeBookingByCampId(campId);
                            context.Camps.Attach(requiredCamp);
                            context.Camps.Remove(requiredCamp);

                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return requiredCamp;
                }
                else
                {
                    return null;
                }


            }

           


        }

        public List<Camp> getFilteredCamps(List<Guid> bookedCamps, int capacity)
        {
            List<Camp> availableCamps = new List<Camp>();
            using (var context = new CampDBEntities())
            {
                return context.Camps.Where(s => !bookedCamps.Contains(s.Id) && s.Capacity == capacity).ToList();
            }

        }
    }
}
