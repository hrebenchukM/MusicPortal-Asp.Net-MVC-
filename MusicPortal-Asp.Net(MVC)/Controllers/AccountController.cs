using MusicPortal_Asp.Net_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IPassword passwordService;
        public AccountController(IUserService userserv, IPassword ps)
        {
            userService = userserv;
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
                if (!await userService.AnyUsers())
                {
                    ModelState.AddModelError("", "No users found.");
                    return View(logon);
                }
                var user = await userService.GetUserByLogin(logon.Login);
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
                UserDTO user = new UserDTO();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.Role = "User";
                user.IsActive = false;
                user.Salt = passwordService.GenerateSalt(); ;
                user.Password = passwordService.HashPassword(user.Salt, reg.Password);

                await userService.CreateUser(user);
                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}