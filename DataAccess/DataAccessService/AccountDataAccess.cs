using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessModel;

namespace DataAccess.DataAccessService
{
    //dummy class contains userName and password
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AccountDataAccess
    {
        //return true if the user's info is correct
        public bool Login(User userEntity)
        {
            using (var context = new CampDBEntities())
            {
                
                bool isValidUser = context.Users.Any(User => User.Email.ToLower() ==
                userEntity.Email.ToLower() && User.Password == userEntity.Password && User.IsAdmin);

                return isValidUser;
            }
        }

        //check db if the given user exits in the database or not
        public bool UserExists(User userEntity)
        {
            using (var context = new CampDBEntities())
            {
                bool isValidUser = context.Users.Any(User => User.Email.ToLower() ==
                userEntity.Email.ToLower());

                return isValidUser;
            }
        }

        //checks if given user is valid or not
        public bool ValidateUser(string userName, string password)
        {

            using (var context = new CampDBEntities())
            {
                bool isValidUser = context.Users.Any(User => User.Email.ToLower() ==
                userName.ToLower() && User.Password == password);

                return isValidUser;
            }
        }

       
        //get userId and password for a user
        public UserInfo GetUserInfo()
        {
            using(var context = new CampDBEntities())
            {
                var userName = context.Users.Select(s => s.Email).FirstOrDefault();
                var password = context.Users.Select(s => s.Password).FirstOrDefault();

                UserInfo userInfo = new UserInfo();
                userInfo.UserName = userName;
                userInfo.Password = password;

                return userInfo;
            }
        }

       
    }
}
