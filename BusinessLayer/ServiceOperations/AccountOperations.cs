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
    public class AccountOperations
    {
        public bool Login(AccountModel accountModel)
        {

            User user = new User();

            user.Email = accountModel.Email;
            user.Password = accountModel.Password;
            user.Id = new Guid();

            AccountDataAccess accountDataAccess = new AccountDataAccess();
            return accountDataAccess.Login(user);

        }

        //check if the given user is authenticated or not
        public bool IsUserValid(string userName, string password)
        {
            AccountDataAccess accountDataAccess = new AccountDataAccess();
            return accountDataAccess.ValidateUser(userName,password);
        }

        //get the user information such as userName and password
        public UserInfo GetUserInfo()
        {
            AccountDataAccess accountDataAccess = new AccountDataAccess();
            return  accountDataAccess.GetUserInfo();
            
        }

    }
}
