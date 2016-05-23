using System.Security.Principal;
using System.Web.Security;

namespace CustomFormAuth.Helpers
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly CustomIdentity customIdentity;
        public CustomPrincipal(CustomIdentity _myIdentity)
        {
            customIdentity = _myIdentity;
        }
        public IIdentity Identity
        {
            get { return customIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}