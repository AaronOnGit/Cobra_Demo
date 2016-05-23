using CustomFormAuth.Entities;
using CustomFormAuth.Helpers;
using CustomFormAuth.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using CustomFormAuth.HelperClass;
using System.Web.Script.Serialization;
using System.Web;

namespace CustomFormAuth.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Accounts/Register
        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }

        // POST: Accounts/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FormAuthenticationEntities())
                {
                    var endcodeKey = Helper.GeneratePassword(10);
                    var hashPassword = Helper.EncodePassword(model.Password, endcodeKey);

                    var entity = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.Username,
                        Password = hashPassword,
                        EncodeKey = endcodeKey,
                        CreateBy = "user",
                        CreateOn = DateTime.Now.AddYears(-3)
                    };
                    try
                    {
                        db.Users.Add(entity);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return View(model);
        }

        // GET: Accounts/Login
        public ActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        // POST: Accounts/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model, string returnUrl="")
        {
            if (ModelState.IsValid)
            {
                var isValidUser = new CustomMembershipProvider().ValidateUser(model.UserName, model.Password);
                if (isValidUser)
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, model.Remember);
                    User user = null;
                    using (var db = new FormAuthenticationEntities())
                    {
                        user = db.Users.Where(u => u.UserName == model.UserName).SingleOrDefault();

                        if (user != null)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string data = js.Serialize(user);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), model.Remember, data);
                            string encToken = FormsAuthentication.Encrypt(ticket);
                            HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                            Response.Cookies.Add(authoCookies);
                        }
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

            }
            return View(model);
        }

        // GET: Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}