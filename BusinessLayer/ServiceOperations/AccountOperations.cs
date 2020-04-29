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
                
                user.Email =accountModel.Email;
                user.Password = accountModel.Password;
                user.Id = new Guid();
                
                AccountDataAccess accountDataAccess = new AccountDataAccess();
                return accountDataAccess.Login(user);
            
        }

       

        public string[] GetRolesOfUser(string Email)
        {
            AccountDataAccess accountDataAccess = new AccountDataAccess();
            return accountDataAccess.GetUserRoles(Email);
        }

        
    }
}
