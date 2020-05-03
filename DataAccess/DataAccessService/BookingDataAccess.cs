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

        public void removeBookingByCampId(Guid campId)
        {
            using(var context = new CampDBEntities())
            {
                var requiredBookings = context.Bookings.Where(bookings => bookings.CampId == campId).ToList();
                foreach(var booking in requiredBookings)
                {
                    context.Bookings.Remove(booking);
                }
                context.SaveChanges();
            }
        }

       

        public List<Guid> campsBetween(DateTime checkIn, DateTime checkOut)
        {
            using (var context = new CampDBEntities())
            {
                var result = context.Bookings
                    .Where(s => (checkIn <= s.CheckInDate && s.CheckOutDate <= checkOut) ||
                        (checkIn <= s.CheckInDate && s.CheckOutDate <= checkOut) || (checkIn >= s.CheckInDate && checkOut <= s.CheckOutDate))
                    .Select(s => s.CampId).ToList();
                return result;
            }

        }
    }
}
