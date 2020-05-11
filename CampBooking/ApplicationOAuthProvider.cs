using BusinessLayer.ServiceOperations;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CampBooking
{
    /// <summary>
    /// authentication class to validate and grand resources is the user is valid
    /// </summary>
    public class ApplicationOAuthProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AccountOperations accountOperations = new AccountOperations();
            var isValidUser = accountOperations.IsUserValid(context.UserName,context.Password);
            var userinfo = accountOperations.GetUserInfo();

            if (isValidUser)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("username", userinfo.UserName));
                identity.AddClaim(new Claim("password", userinfo.Password));
                context.Validated(identity);
            }
            else {
                return;
            }
                
        }
    }
}