using DataAccess.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessService
{
    public class BookingDataAccess
    {
        public string random()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        //generate booking ref no.
        public string CreateBooking(Booking booking)
        {
            using (var context = new CampDBEntities())
            {
                //booking.UserId = GetUserId(email);
                booking.ReferenceNumber = random();

                bool IsBooked = (from c in context.Camps
                                 where booking.CampId == c.Id
                                 select c.IsBooked).FirstOrDefault();
                IsBooked = true;

                context.Bookings.Add(booking);




                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex) { throw ex; }
                return booking.ReferenceNumber;

            }
        }

        private Guid GetUserId(string email)
        {
            using (var context = new CampDBEntities())
            {
                Guid result = (from user in context.Users
                               where user.Email == email
                               select user.Id).FirstOrDefault();
                return result;

            }
        }

        //Return the details booking event based on reference number
        public Booking fetchBooking(string BookingReferenceNumber)
        {
            using (var context = new CampDBEntities())
            {
                var requiredBooking = context.Bookings.Where(s => s.ReferenceNumber == BookingReferenceNumber).FirstOrDefault();
                return requiredBooking;
            }

        }

        public Booking fetchBookingForUser(Guid userId)
        {
            using (var context = new CampDBEntities())
            {
                return context.Bookings.Where(bookings => userId == bookings.UserId).FirstOrDefault();
            }
        }

        //Cancel only future date bookings Make the camp visible on the dashborad for availabe dates
        public bool CancelBooking(string BookingReferenceNumber)
        {
            using (var context = new CampDBEntities())
            {
                var requiredBooking = context.Bookings.Where(s => s.ReferenceNumber == BookingReferenceNumber).FirstOrDefault();
                if (requiredBooking != null && requiredBooking.CheckInDate > DateTime.Now)
                {
                    bool IsBooked = (from camp in context.Camps
                                     where requiredBooking.CampId == camp.Id
                                     select camp.IsBooked).FirstOrDefault();

                    IsBooked = false;

                    context.Bookings.Remove(requiredBooking);
                    context.SaveChanges();
                    return true;
                }

                else
                {
                    return false;
                }



            }
        }

        public List<Camp> GetCamps(DateTime checkin, DateTime checkout, int capacity)
        {
            List<Camp> availableCamps = new List<Camp>();
            using (var context = new CampDBEntities())
            {
                var campIds = context.Bookings.Where(bookings => bookings.CheckInDate < checkin && bookings.CheckOutDate > checkout).Select(booking => booking.CampId);


                var totalCamps = context.Camps.Where(c => c.Capacity == capacity).ToList();

                foreach (var camp in totalCamps)
                {
                    bool isBooked = false;
                    foreach (var campId in campIds)
                    {
                        if (camp.Id == campId)
                        {
                            isBooked = true;
                        }
                    }
                    if (isBooked == false)
                    {
                        availableCamps.Add(camp);
                    }
                }
            }
            return availableCamps;
        }
    }
}
