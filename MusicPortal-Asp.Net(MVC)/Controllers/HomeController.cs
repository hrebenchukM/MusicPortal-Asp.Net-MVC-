using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal_Asp.Net_MVC_.Models;
using MusicPortal_Asp.Net_MVC_.Repository;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _urepo;

        private readonly ISongRepository _srepo;
        private readonly IGenreRepository _grepo;
        private readonly IArtistRepository _arepo;
        IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger,  IUserRepository ur, ISongRepository sr, IWebHostEnvironment appEnvironment, IGenreRepository gr, IArtistRepository ar)
        {
            _logger = logger;
            _urepo = ur;
            _srepo = sr;
            _appEnvironment = appEnvironment;
            _grepo = gr;
            _arepo = ar;
        }

        public async Task <IActionResult> Index()
        {
            string? login = HttpContext.Session.GetString("Login") ?? Request.Cookies["login"];
            if (login != null)
            {
                HttpContext.Session.SetString("Login", login); 
                var msgContext = await _urepo.GetList();
                ViewBag.UserId = new SelectList(msgContext, "Id", "Login");

                var songs = await _srepo.GetList();



                string? role = HttpContext.Session.GetString("Role") ?? Request.Cookies["role"];
                if (role == "Admin")
                {
                    ViewBag.IsAdmin = role == "Admin";
                    return RedirectToAction("Index", "Admin");

                }
               
                return View(songs);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            Response.Cookies.Delete("login");
            Response.Cookies.Delete("role");
            return RedirectToAction("Login", "Account");
        }
        public IActionResult GetFile(string fileName)
        {
            // Путь к файлу
            string file_path = fileName;
            // Тип файла - content-type
            string file_type = "audio/mpeg";
            // Имя файла - необязательно
            string file_name = "song.mp3";
            return File(file_path, file_type, file_name);
        }




        public async Task<IActionResult> DetailsSongU(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _srepo.Get(id.Value);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public async Task<IActionResult> CreateSongUAsync()
        {
            var artistList = await _arepo.GetList();
            var genreList = await _grepo.GetList();
            var userList = await _urepo.GetList();

            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "Login");


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> CreateSongU([Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] Song song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
        {
            string? role = HttpContext.Session.GetString("Role") ?? Request.Cookies["role"];
            string? login = HttpContext.Session.GetString("Login") ?? Request.Cookies["login"];
            if (role == "Admin")
            {
                song.UserId = (int)await _srepo.GetUserIdByRole("Admin");
            }
            else if (!string.IsNullOrEmpty(login))
            {
                song.UserId = (int)await _srepo.GetUserIdByLogin(login);
            }
            if (uploadedFileP == null)
                ModelState.AddModelError("", "Необходимо загрузить постер.");
            if (uploadedFileV == null)
                ModelState.AddModelError("", "Необходимо загрузить клип.");

            if (uploadedFileS == null)
                ModelState.AddModelError("", "Необходимо загрузить песню.");


            if (ModelState.IsValid)
            {
                if (uploadedFileP != null)
                {
                    string pathP = "/Files/" + uploadedFileP.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathP, FileMode.Create))
                    {
                        await uploadedFileP.CopyToAsync(fileStream);
                    }
                    song.PathP = pathP;
                }

                if (uploadedFileV != null)
                {
                    string pathV = "/Files/" + uploadedFileV.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathV, FileMode.Create))
                    {
                        await uploadedFileV.CopyToAsync(fileStream);
                    }
                    song.PathV = pathV;
                }

                if (uploadedFileS != null)
                {
                    string pathS = "/Files/" + uploadedFileS.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathS, FileMode.Create))
                    {
                        await uploadedFileS.CopyToAsync(fileStream);
                    }
                    song.PathS = pathS;
                }
                await _srepo.Create(song);
                await _srepo.Save();
                return RedirectToAction(nameof(Index));
            }
            var artistList = await _arepo.GetList();
            var genreList = await _grepo.GetList();
            var userList = await _urepo.GetList();

            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "Login");

            return View(song);
        }



      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
