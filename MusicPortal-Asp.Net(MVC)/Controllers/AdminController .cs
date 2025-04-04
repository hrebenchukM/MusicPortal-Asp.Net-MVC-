
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISongService songService;
        private readonly IArtistService artistService;//работаем с тим сервисом 
        private readonly IGenreService genreService;
        private readonly IUserService userService;
        IWebHostEnvironment _appEnvironment;
        public AdminController(ISongService songserv, IArtistService artistserv, IGenreService genreserv, IUserService userserv, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            songService = songserv;
            artistService = artistserv;
            genreService = genreserv;
            userService = userserv;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await songService.GetSongs();
            return View(songs);
        }


        public async Task<IActionResult> IndexUsers()
        {
            var users = await userService.GetUsers(); 
            return View(users);
        }


        public async Task<IActionResult> NoActiveUsers()
        {
            var noActiveUsers = await userService.GetInactiveUsers();
            return View(noActiveUsers);
        }

        public async Task<IActionResult> ChangeActiveStatusUser(int id)
        {
            await userService.ChangeActiveStatus(id);
           
            return RedirectToAction(nameof(NoActiveUsers)); 
        }




        public async Task<IActionResult> DetailsUser(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                UserDTO user = await userService.GetUser((int)id);

                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        public async Task<IActionResult> EditUser(int? id)
        {
            try
            {
                if (id == null)
                {
                  return NotFound();
                }

                var user = await userService.GetUser((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,FirstName,LastName,Login,Password,Salt,Role,IsActive")] UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await userService.UpdateUser(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }



        public IActionResult CreateUser()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("Id,FirstName,LastName,Login,Password,Salt,Role,IsActive")] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await userService.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                UserDTO user = await userService.GetUser((int)id);
                return View(user);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUser(int id)
        {
           
            await userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> UserExists(int id)
        {
            return await userService.ExistsUser(id);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> IndexGenres()
        {
            var genres = await genreService.GetGenres();
            return View(genres);
        }

        public async Task<IActionResult> DetailsGenre(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                GenreDTO genre = await genreService.GetGenre((int)id);
                return View(genre);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }

         
        }

     
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Name")] GenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await genreService.CreateGenre(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public async Task<IActionResult> EditGenre(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                GenreDTO genre = await genreService.GetGenre((int)id);
           
                return View(genre);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, [Bind("Id,Name")] GenreDTO genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await genreService.UpdateGenre(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenreExists(genre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public async Task<IActionResult> DeleteGenre(int? id)
        {
            try
            {
                if (id == null)
                {
                   return NotFound();
                }

                GenreDTO genre = await genreService.GetGenre((int)id);
                return View(genre);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("DeleteGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedGenre(int id)
        {
            GenreDTO genre = await genreService.GetGenre((int)id);
            if (genre != null)
            {
                await genreService.DeleteGenre(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExists(int id)
        {
            return await genreService.ExistsGenre(id);
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> IndexArtists()
        {
            var artists = await artistService.GetArtists();
            return View(artists);
        }

        public async Task<IActionResult> DetailsArtist(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                ArtistDTO artist = await artistService.GetArtist((int)id);
           
                return View(artist);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public IActionResult CreateArtist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArtist([Bind("Id,Name")] ArtistDTO artist)
        {
            if (ModelState.IsValid)
            {
                await artistService.CreateArtist(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        public async Task<IActionResult> EditArtist(int? id)
        {

            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                ArtistDTO artist = await artistService.GetArtist((int)id);
                return View(artist);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtist(int id, [Bind("Id,Name")] ArtistDTO artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await artistService.UpdateArtist(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArtistExists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        public async Task<IActionResult> DeleteArtist(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                ArtistDTO artist = await artistService.GetArtist((int)id);
                return View(artist);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

   
        [HttpPost, ActionName("DeleteArtist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedArtist(int id)
        {

            ArtistDTO artist = await artistService.GetArtist((int)id);

            if (artist != null)
            {
                await artistService.DeleteArtist(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistExists(int id)
        {
            return await artistService.ExistsArtist(id);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

      
        public async Task<IActionResult> DetailsSong(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async Task <IActionResult> CreateSong()
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
        public async Task<IActionResult> CreateSong([Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] SongDTO song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
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


            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name", song.GenreId);
            ViewData["UserId"] = new SelectList(userList, "Id", "Login", song.UserId);


            return View(song);
        }

        public async Task<IActionResult> EditSong(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var song = await songService.GetSong((int)id);
          
            var artistList = await artistService.GetArtists();
            var genreList = await genreService.GetGenres();
            var userList = await userService.GetUsers();


            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name",song.ArtistId);
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name",song.GenreId);
            ViewData["UserId"] = new SelectList(userList, "Id", "Login",song.UserId);
            return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(int id, [Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] SongDTO song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
        {
            if (id != song.Id)
            {
                return NotFound();
            }
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

                try
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
                  await songService.UpdateSong(song);
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await songService.ExistsSong(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var artistList = await artistService.GetArtists();
            var genreList = await genreService.GetGenres();
            var userList = await userService.GetUsers();


            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name", song.ArtistId);
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name", song.GenreId);
            ViewData["UserId"] = new SelectList(userList, "Id", "Login", song.UserId);

            return View(song);
        }

        public async Task<IActionResult> DeleteSong(int? id)
        {
            try
            {
                if (id == null)
                {
                   return NotFound();
                }

                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSong(int id)
        {
            await songService.DeleteSong(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
