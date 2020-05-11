using BusinessLayer.ServiceModels;
using BusinessLayer.ServiceOperations;
using CampBooking.Models;
using System;
using System.Web.Http;

namespace CampBooking.Controllers
{
    [RoutePrefix("Api/Login")]
    public class AccountController : ApiController
    {
        private readonly AccountOperations accountOperations;
        public AccountController()
        {
            accountOperations = new AccountOperations();
        }

       
        
        [HttpPost]
        [Route("AccountLogin")]
        public IHttpActionResult PostLogin(AccountViewModel accountViewModel)
        {

           
            AccountModel accountModel = new AccountModel()
            {
                Email = accountViewModel.Email,

                Password = accountViewModel.Password,

            };

            try
            {
                if (!accountOperations.Login(accountModel))
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(accountViewModel);
        }

    }

    
}
