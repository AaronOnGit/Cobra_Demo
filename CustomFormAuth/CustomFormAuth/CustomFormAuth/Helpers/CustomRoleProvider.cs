using CustomFormAuth.Entities;
using CustomFormAuth.HelperClass;
using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;


namespace CustomFormAuth.Helpers
{
    public class CustomRoleProvider : RoleProvider
    {
        private int _cacheTimeoutInMinute = 20;
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            //throw new NotImplementedException();
            using(var db = new FormAuthenticationEntities())
            {
                var isExist = RoleExists(roleName);
                if(!isExist)
                {
                    var role = new Role
                    {
                        RoleName = roleName,
                        CreateBy = Helper.GetCurrentUserName(),
                        CreateOn = DateTime.Now.AddYears(-3)
                    };
                }
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if(RoleExists(roleName))
            {
                using(var db = new FormAuthenticationEntities())
                {
                    var role = db.Roles.Where(r => r.RoleName.Equals(roleName)).SingleOrDefault();
                    db.Roles.Remove(role);
                    return true;
                }
            }
            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            //check cache
            var cacheKey = string.Format("{0}_role", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[])HttpRuntime.Cache[cacheKey];
            }
            string[] roles = new string[] { };
            using (var db = new FormAuthenticationEntities())
            {
                roles = (from r in db.Roles
                         join ur in db.UserRoles on r.Id equals ur.RoleId
                         join u in db.Users on ur.UserId equals u.Id
                         where u.UserName.Equals(username)
                         select r.RoleName).ToArray<string>();
                if (roles.Count() > 0)
                {
                    HttpRuntime.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);

                }
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            //throw new NotImplementedException();
            if (String.IsNullOrEmpty(roleName))
                return false;
            using(var db = new FormAuthenticationEntities())
            {
                var role = db.Roles.Where(r => r.RoleName == roleName).SingleOrDefault();
                if(role != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}