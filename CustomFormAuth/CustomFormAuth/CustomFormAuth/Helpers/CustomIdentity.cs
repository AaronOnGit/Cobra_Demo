using CustomFormAuth.Entities;
using System.Security.Principal;

namespace CustomFormAuth.Helpers
{
    public class CustomIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public User User { get; set; }

        public CustomIdentity(User user)
        {
            Identity = new GenericIdentity(user.UserName);
            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}