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
    [Route("api/Songs")]
    public class SongsController : ControllerBase//класс по сути является WebAPI службой
    {
        private readonly ISongService songService;
        IWebHostEnvironment _appEnvironment;
        public SongsController(ISongService songserv, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            songService = songserv;
        }



        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()//все само конвертируется в формат json
        {
            var songs = await songService.GetSongs();
            return Ok(songs);
        }


        // GET: api/Songs/3
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            SongDTO song = await songService.GetSong((int)id);

            if (song == null)
            {
                return NotFound();
            }
            return new ObjectResult(song);

        }

        // PUT: api/Songs
        [HttpPut]
        public async Task<ActionResult<SongDTO>> PutSong(SongDTO song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await songService.ExistsSong(song.Id))
            {
                return NotFound();
            }

            await songService.UpdateSong(song);
            return Ok(song);
        }



        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO song)//заполняем без id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var created = await songService.CreateSong(song);
            return Ok(created);
           
        }

        // DELETE: api/Songs/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<SongDTO>> DeleteSong(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SongDTO song = await songService.GetSong((int)id);
  
            if (song == null)
            {
                return NotFound();
            }


            await songService.DeleteSong(id);

            return Ok(song);
        }
    }
}
