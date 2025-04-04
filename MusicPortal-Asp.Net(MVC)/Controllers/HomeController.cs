using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.Models;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService songService;
        private readonly IArtistService artistService;//работаем с тим сервисом 
        private readonly IGenreService genreService;
        private readonly IUserService userService;
        IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger,  IWebHostEnvironment appEnvironment, ISongService songserv, IArtistService artistserv, IGenreService genreserv, IUserService userserv)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            songService = songserv;
            artistService = artistserv;
            genreService = genreserv;
            userService = userserv;
        }

        public async Task <IActionResult> Index(string artist, int genre = 0, int page = 1,//по умолчанию приходит страница первый раз 1////идентификатор команды и амплуа
            SortState sortOrder = SortState.TitleAsc)
        {
          

            string? login = HttpContext.Session.GetString("Login") ?? Request.Cookies["login"];
            if (login != null)
            {
                HttpContext.Session.SetString("Login", login); 
                var msgContext = await userService.GetUsers();
                ViewBag.UserId = new SelectList(msgContext, "Id", "Login");
                int pageSize = 4; // количество элементов на странице

                //фильтрация
                IQueryable<SongDTO> songs = (IQueryable<SongDTO>)songService.GetSongsIQueryable();//получаем всю коллекцию игроков

                if (genre != 0)
                {
                    songs = songs.Where(p => p.GenreId == genre);//фильтрация по команде
                }
                if (!string.IsNullOrEmpty(artist))
                {
                    songs = songs.Where(p => p.Artist == artist);//фильтрация по позиции
                }




                // сортировка
                songs = sortOrder switch
                {
                    SortState.TitleDesc => songs.OrderByDescending(s => s.Title),
                    SortState.YearAsc => songs.OrderBy(s => s.Year),
                    SortState.YearDesc => songs.OrderByDescending(s => s.Year),
                    SortState.ArtistAsc => songs.OrderBy(s => s.Artist!),
                    SortState.ArtistDesc => songs.OrderByDescending(s => s.Artist!),
                    SortState.GenreAsc => songs.OrderBy(s => s.Genre!),
                    SortState.GenreDesc => songs.OrderByDescending(s => s.Genre!),
                    _ => songs.OrderBy(s => s.Title),
                };

                // пагинация
                var count = await songs.CountAsync();//кол-во игроков
                var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();



                // формируем модель представления
                IndexViewModel viewModel = new IndexViewModel(
                    items,
                new PageViewModel(count, page, pageSize),
                    new FilterViewModel((await genreService.GetGenres()).ToList(), genre, artist),
                    new SortViewModel(sortOrder)
                );


                string? role = HttpContext.Session.GetString("Role") ?? Request.Cookies["role"];
                if (role == "Admin")
                {
                    ViewBag.IsAdmin = role == "Admin";
                    return RedirectToAction("Index", "Admin");

                }

                //return View(songs);
                return View(viewModel);
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

            SongDTO song = await songService.GetSong((int)id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public async Task<IActionResult> CreateSongUAsync()
        {
            var artistList = await artistService.GetArtists();
            var genreList = await genreService.GetGenres();
            var userList = await userService.GetUsers();


            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "Login");


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> CreateSongU([Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] SongDTO song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
        {
            string? role = HttpContext.Session.GetString("Role") ?? Request.Cookies["role"];
            string? login = HttpContext.Session.GetString("Login") ?? Request.Cookies["login"];
            if (role == "Admin")
            {
                song.UserId = (int)await songService.GetUserIdByRole("Admin");
            }
            else if (!string.IsNullOrEmpty(login))
            {
                song.UserId = (int)await songService.GetUserIdByLogin(login);
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
                await songService.CreateSong(song);
                return RedirectToAction(nameof(Index));
            }
        
            var artistList = await artistService.GetArtists();
            var genreList = await genreService.GetGenres();
            var userList = await userService.GetUsers();



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
