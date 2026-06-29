using DataBaseFirst;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EscuelaContext _context;
        public UserController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll() => await _context.Users.ToListAsync();

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usr = await _context.Users.FindAsync(id);
            if (usr == null)
                return NotFound();

            return Ok(usr);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUsers(Usuario usr)
        {
            _context.Users.Add(usr);
            await _context.SaveChangesAsync();
            return Ok(usr);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, Usuario usr)
        {
            if (usr is not null)
            {
                if (usr.UserId != id)
                    return BadRequest();

                _context.Entry(usr).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _context.Users.AnyAsync(e => e.UserId == id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return Ok(usr);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult<Usuario>> RemoveUser(int id)
        {
            if (id == 0)
                return BadRequest();

            var usr = await _context.Users.FindAsync(id);
            if (usr == null)
                return NotFound();

            _context.Users.Remove(usr);
            await _context.SaveChangesAsync();

            return Ok(usr);
        }
    }
}
