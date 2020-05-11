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
        //generates a random 8 digit alpha numeric code 
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

        //returns the random booking ref. number for the created camp
        public string CreateBooking(Booking booking)
        {
            using (var context = new CampDBEntities())
            {
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

        //return userId using its email
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

        //fetches the booking for a user using its userId
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

        //removes a booking using its campId
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

       //list of available camps between checkIn and checkOut date
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

        /// <summary>
        /// 1. Assign the rating to the booked camp with given reference number
        /// 2. Find average of all the non zero ratings for that camp to display on dashboard
        /// </summary>
        /// <param name="referenceNumber"></param> booking reference number for that camp
        /// <param name="rating"></param> Rating is take as a nullable parameter here
        public void AddRating(string referenceNumber, int rating)
        {

            using (var context = new CampDBEntities())
            {
                var requiredBooking = context.Bookings.FirstOrDefault(s => s.ReferenceNumber == referenceNumber);

                requiredBooking.Rating = rating;
               

                context.Entry(requiredBooking).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
                int?[] ratings = context.Bookings.Where(s => s.Rating != 0 && s.CampId == requiredBooking.CampId).Select(s => s.Rating).ToArray();
                

                List<int> ratings2 = new List<int>();
                foreach (var item in ratings)
                {
                    if (item.HasValue)
                    {
                        ratings2.Add(item ?? default(int));   // add only if it has a non-null value
                    }
                }

                int roundedRating = (int)Math.Round(ratings2.Average(), MidpointRounding.AwayFromZero);

                var requiredCamp = context.Camps.First(s => s.Id == requiredBooking.CampId);

                requiredCamp.Rating = roundedRating;
                //assign rating to the required camp

                context.Entry(requiredCamp).State = System.Data.Entity.EntityState.Modified;


                context.SaveChanges();


            }
               
        }
    }
}
