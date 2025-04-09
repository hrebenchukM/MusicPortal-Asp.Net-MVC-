using MusicPortal_Asp.Net_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.Filters;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{
    [Culture]//собственный атрибут - фильтр действия,срабатывает для каждого екшена перед . Изменяет культуру,прочитывает соответсвующую таблицу из файла ресурсов

    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IPassword passwordService;
        private readonly ILangRead _langRead;
        public AccountController(IUserService userserv, IPassword ps, ILangRead langRead)
        {
            userService = userserv;
            passwordService = ps;
            _langRead = langRead;
        }
        public ActionResult ChangeCulture(string lang)//приходит значение выбранное в комбобоксе
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";//хочу знать где я был на каком маршруте,если в сессии ничего нет то по умолчанию пусть будет /Club/Index

            // Список культур, снова обращаемся к сервису чтоб понять поддерживается ли культура что пришла
            List<string> cultures = _langRead.languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(lang))
            {
                lang = "uk";
            }
            //новуя культуру что мы изменили мы записываем в куки на 10 дней
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки(формирование служебных заголовков)Когда браузер получит ответ тогда сохранит культуру на клиенской стороне куки
            return Redirect(returnUrl);//редирект туда где мы были ибо мастер страница 
        }
        public IActionResult LoginAsGuest()
        {
            HttpContext.Session.SetString("path", Request.Path);
            HttpContext.Session.SetString("Login", "Guest");
            HttpContext.Session.SetString("FirstName", "Guest");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            HttpContext.Session.SetString("path", Request.Path);

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
            HttpContext.Session.SetString("path", Request.Path);
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