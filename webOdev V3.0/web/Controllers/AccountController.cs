using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using web.DAL;
using web.Models;

//İDENTİTY İLE GELEN CONTROLLER İÇİNDE DÜZENLEMELER YAPILDI


namespace web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        
       
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

      
       // LOGİN VİEW MODELE POST EDEN CONTROLLER
       //ALLOWANONYMOUS ANONİMLERİN DE GİRİŞ YAPABİLMESİNİ SAĞLAR 
       //HTTP POST AŞAMASINDA DIŞARIDAN GELEN SALDIRILARI ENGELLER ,MVC MUHAFIZI.

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginViewModel user = new LoginViewModel() { KullaniciAdi=model.KullaniciAdi,Email = model.Email, Password = model.Password };

            user = Repository.GetUserDetails(user);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.KullaniciAdi, false);

                var authTicket = new FormsAuthenticationTicket(1, user.KullaniciAdi, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("", "Giriş Yapılamadı");
                return View(model);
            }
        }
        
        

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

      
        // STANDART USER TANIMLAMA KAYIT OLMA İŞLEMİ İÇİN GEREKLİ CONTROLLER 

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.KullaniciAdi, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                   
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

       
            return View(model);
        }

        // ÇIKIŞ YAPTIĞIN ZAMAN İNDEX HOME A GÖNDERİYOR

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {       
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

       
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Yardımcılar
        
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}