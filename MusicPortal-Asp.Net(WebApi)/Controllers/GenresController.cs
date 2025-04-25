using Microsoft.AspNetCore.Mvc;//все равно надо 

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
using MusicPortal_Asp.Net_MVC_.BLL.Services;
namespace MusicPortal_Asp.Net_WebApi_.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase//класс по сути является WebAPI службой
    {
        private readonly IGenreService genreService;
        public GenresController(IGenreService genreserv)
        {
            genreService = genreserv;
        }



        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()//все само конвертируется в формат json
        {
            var artists = await genreService.GetGenres();
            return Ok(artists);
        }


        // GET: api/Genres/3
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            GenreDTO genre = await genreService.GetGenre((int)id);

            if (genre == null)
            {
                return NotFound();
            }
            return new ObjectResult(genre);

        }

        // PUT: api/Genres
        [HttpPut]
        public async Task<ActionResult<GenreDTO>> PutGenre(GenreDTO genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await genreService.ExistsGenre(genre.Id))
            {
                return NotFound();
            }

            await genreService.UpdateGenre(genre);
            return Ok(genre);
        }



        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genre)//заполняем без id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await genreService.CreateGenre(genre);
            return Ok(genre);
        }

        // DELETE: api/Genres/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> DeleteArtist(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GenreDTO genre = await genreService.GetGenre((int)id);

            if (genre == null)
            {
                return NotFound();
            }


            await genreService.DeleteGenre(id);

            return Ok(genre);
        }
    }
}
