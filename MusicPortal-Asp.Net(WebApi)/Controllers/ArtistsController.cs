using Microsoft.AspNetCore.Mvc;//все равно надо 

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal_Asp.Net_MVC_.BLL.DTO;
using MusicPortal_Asp.Net_MVC_.BLL.Interfaces;
using MusicPortal_Asp.Net_MVC_.BLL.Infrastructure;
using MusicPortal_Asp.Net_MVC_.DAL.Entities;
namespace MusicPortal_Asp.Net_WebApi_.Controllers
{
    [ApiController]
    [Route("api/Artists")]
    public class ArtistsController : ControllerBase//класс по сути является WebAPI службой
    {
        private readonly IArtistService artistService;
        public ArtistsController(IArtistService artistserv)
        {
            artistService = artistserv;
        }



        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()//все само конвертируется в формат json
        {
            var artists = await artistService.GetArtists();
            return Ok(artists);
        }


        // GET: api/Artists/3
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            ArtistDTO artist = await artistService.GetArtist((int)id);

            if (artist == null)
            {
                return NotFound();
            }
            return new ObjectResult(artist);

        }

        // PUT: api/Artists
        [HttpPut]
        public async Task<ActionResult<ArtistDTO>> PutArtist(ArtistDTO artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await artistService.ExistsArtist(artist.Id))
            {
                return NotFound();
            }

            await artistService.UpdateArtist(artist);
            return Ok(artist);
        }



        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist(ArtistDTO artist)//заполняем без id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await artistService.CreateArtist(artist);
            return Ok(artist);
        }

        // DELETE: api/Artists/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtistDTO>> DeleteArtist(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ArtistDTO artist = await artistService.GetArtist((int)id);

            if (artist == null)
            {
                return NotFound();
            }


            await artistService.DeleteArtist(id);

            return Ok(artist);
        }
    }
}
