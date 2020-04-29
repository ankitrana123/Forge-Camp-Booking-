using System.Web.Http.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BusinessLayer.ServiceOperations;

namespace CampBooking
{
   
        public class UserRoleProvider : RoleProvider
        {
            public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override void AddUsersToRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override void CreateRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
            {
                throw new NotImplementedException();
            }

            public override string[] FindUsersInRole(string roleName, string usernameToMatch)
            {
                throw new NotImplementedException();
            }

            public override string[] GetAllRoles()
            {
                throw new NotImplementedException();
            }

            public override string[] GetRolesForUser(string Email)
            {
            AccountOperations accountOperations = new AccountOperations();
            var userRoles = accountOperations.GetRolesOfUser(Email);
            return userRoles;

            }

            public override string[] GetUsersInRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool IsUserInRole(string username, string roleName)
            {
                throw new NotImplementedException();
            }

            public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override bool RoleExists(string roleName)
            {
                throw new NotImplementedException();
            }
        }
    }
