using MusicPortal_Asp.Net_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using MusicPortal_Asp.Net_MVC_.Repository;
using MusicPortal_Asp.Net_MVC_.Services;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserRepository repo;
        private readonly IPassword passwordService;
        public AccountController(IUserRepository r, IPassword ps)
        {
            repo = r;
            passwordService = ps;
        }
        public IActionResult LoginAsGuest()
        {
            HttpContext.Session.SetString("Login", "Guest");
            HttpContext.Session.SetString("FirstName", "Guest");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if (!await repo.AnyUsers())
                {
                    ModelState.AddModelError("", "No users found.");
                    return View(logon);
                }
                var user = await repo.GetUserByLogin(logon.Login);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login!");
                    return View(logon);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "Your account is not activated.");
                    return View(logon);
                }

                string? salt = user.Salt;


                if (user.Password != passwordService.HashPassword(salt, logon.Password))
                {
                    ModelState.AddModelError("", "Wrong password!");
                    return View(logon);
                }

               
                HttpContext.Session.SetString("Login", user.Login);
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("Role", user.Role);

                if (logon.RememberMe)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(10); 
                    Response.Cookies.Append("login", logon.Login, option);
                    Response.Cookies.Append("role", user.Role, option);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.Role = "User";
                user.IsActive = false;
                user.Salt = passwordService.GenerateSalt(); ;
                user.Password = passwordService.HashPassword(user.Salt, reg.Password);

                await repo.Create(user);
                await repo.Save();
                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}