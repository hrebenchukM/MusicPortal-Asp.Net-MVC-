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
    [Route("api/Users")]
    public class UsersController : ControllerBase//класс по сути является WebAPI службой
    {
        private readonly IUserService userService;
        public UsersController(IUserService userserv)
        {
            userService = userserv;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()//все само конвертируется в формат json
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }


        // GET: api/Users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            UserDTO user = await userService.GetUser((int)id);

            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);

        }

        // PUT: api/Users
        [HttpPut]
        public async Task<ActionResult<UserDTO>> PutUser(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await userService.ExistsUser(user.Id))
            {
                return NotFound();
            }

            await userService.UpdateUser(user);
            return Ok(user);
        }



        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)//заполняем без id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userService.CreateUser(user);
            return Ok(user);
        }

        // DELETE: api/Users/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDTO user = await userService.GetUser((int)id);

            if (user == null)
            {
                return NotFound();
            }


            await userService.DeleteUser(id);

            return Ok(user);
        }
    }
}
