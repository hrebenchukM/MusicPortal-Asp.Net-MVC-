using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.Models;
using MusicPortal_Asp.Net_MVC_.Repository;
using MusicPortal_Asp.Net_MVC_.Services;

namespace MusicPortal_Asp.Net_MVC_.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _urepo;
        private readonly IGenreRepository _grepo;
        private readonly IArtistRepository _arepo;
        private readonly ISongRepository _srepo;
        IWebHostEnvironment _appEnvironment;
        public AdminController(IUserRepository ur, IGenreRepository gr, IArtistRepository ar, ISongRepository sr, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _urepo = ur;
            _grepo = gr;
            _arepo = ar;
            _srepo = sr;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await _srepo.GetList();
            return View(songs);
        }


        public async Task<IActionResult> IndexUsers()
        {
            var users = await _urepo.GetList(); 
            return View(users);
        }


        public async Task<IActionResult> NoActiveUsers()
        {
            var noActiveUsers = await _urepo.GetInactiveUsers();
            return View(noActiveUsers);
        }

        public async Task<IActionResult> ChangeActiveStatusUser(int id)
        {
            await _urepo.ChangeActiveStatus(id);
            await _urepo.Save();
           
            return RedirectToAction(nameof(NoActiveUsers)); 
        }




        public async Task<IActionResult> DetailsUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _urepo.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _urepo.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,FirstName,LastName,Login,Password,Salt,Role,IsActive")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _urepo.Update(user);
                    await _urepo.Save();
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
        public async Task<IActionResult> CreateUser([Bind("Id,FirstName,LastName,Login,Password,Salt,Role,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                await _urepo.Create(user);
                await _urepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _urepo.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUser(int id)
        {
            var user = await _urepo.Get(id);
            if (user != null)
            {
                await _urepo.Delete(id);
                await _urepo.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> UserExists(int id)
        {
            return await _urepo.Exists(id);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> IndexGenres()
        {
            var genres = await _grepo.GetList();
            return View(genres);
        }

        public async Task<IActionResult> DetailsGenre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _grepo.Get(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

     
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _grepo.Create(genre);
                await _grepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public async Task<IActionResult> EditGenre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _grepo.Get(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, [Bind("Id,Name")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _grepo.Update(genre);
                    await _grepo.Save();
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
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _grepo.Get(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost, ActionName("DeleteGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedGenre(int id)
        {
            var genre = await _grepo.Get(id);
            if (genre != null)
            {
                await _grepo.Delete(id);
                await _grepo.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExists(int id)
        {
            return await _grepo.Exists(id);
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> IndexArtists()
        {
            var artists = await _arepo.GetList();
            return View(artists);
        }

        public async Task<IActionResult> DetailsArtist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arepo.Get(id.Value);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        public IActionResult CreateArtist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArtist([Bind("Id,Name")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                await _arepo.Create(artist);
                await _arepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        public async Task<IActionResult> EditArtist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arepo.Get(id.Value);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtist(int id, [Bind("Id,Name")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _arepo.Update(artist);
                    await _arepo.Save();
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
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _arepo.Get(id.Value);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

   
        [HttpPost, ActionName("DeleteArtist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedArtist(int id)
        {
            var artist = await _arepo.Get(id);
            if (artist != null)
            {
                await _arepo.Delete(id);
            }

            await _arepo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistExists(int id)
        {
            return await _arepo.Exists(id);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

      
        public async Task<IActionResult> DetailsSong(int? id)
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

        public async Task <IActionResult> CreateSong()
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
        public async Task<IActionResult> CreateSong([Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] Song song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
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

        public async Task<IActionResult> EditSong(int? id)
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
            var artistList = await _arepo.GetList();
            var genreList = await _grepo.GetList();
            var userList = await _urepo.GetList();

            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "Login");
            return View(song);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(int id, [Bind("Id,Title,Year,ArtistId,GenreId,UserId,PathS,PathV,PathP")] Song song, IFormFile uploadedFileP, IFormFile uploadedFileV, IFormFile uploadedFileS)
        {
            if (id != song.Id)
            {
                return NotFound();
            }
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
                    _srepo.Update(song);
                    await _srepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _srepo.Exists(song.Id))
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
            var artistList = await _arepo.GetList();
            var genreList = await _grepo.GetList();
            var userList = await _urepo.GetList();

            ViewData["ArtistId"] = new SelectList(artistList, "Id", "Name");
            ViewData["GenreId"] = new SelectList(genreList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "Login");
            return View(song);
        }

        public async Task<IActionResult> DeleteSong(int? id)
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

        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSong(int id)
        {
            await _srepo.Delete(id);
            await _srepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
