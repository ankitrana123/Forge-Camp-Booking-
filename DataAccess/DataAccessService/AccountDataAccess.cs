using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessModel;

namespace DataAccess.DataAccessService
{
    public class AccountDataAccess
    {
        public bool Login(User userEntity)
        {
            using (var context = new CampDBEntities())
            {
                //bool IsAdminUser = false;
                //if(context.Users.Any(user => user.Email == "ankit.rana@nagarro.co" && user.Password == "abcd")){
                //    IsAdminUser = true;
                //}
                bool isValidUser = context.Users.Any(User => User.Email.ToLower() ==
                userEntity.Email.ToLower() && User.Password == userEntity.Password && User.IsAdmin);

                return isValidUser;
            }
        }

        public bool UserExists(User userEntity)
        {
            using (var context = new CampDBEntities())
            {
                bool isValidUser = context.Users.Any(User => User.Email.ToLower() ==
                userEntity.Email.ToLower());

                return isValidUser;
            }
        }

        public Guid SignUp(User userEntity)
        {
            using (var context = new CampDBEntities())
            {
                context.Users.Add(userEntity);
                
                context.SaveChanges();
                return userEntity.Id;
            }
        }

        public string[] GetUserRoles(string email)
        {
            List<String> Roles = new List<string>();
            using (var context = new CampDBEntities())
            {
                var result = context.Users.Where(s => s.Email == email).Select(s => s.IsAdmin).FirstOrDefault();
                if(result)
                {
                    Roles.Add("Admin");
                }
                else
                {
                    Roles.Add("User");
                }
            }
            return Roles.ToArray();
        }
    }
}
