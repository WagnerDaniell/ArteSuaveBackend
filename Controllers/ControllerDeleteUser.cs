using ASbackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASbackend.Controllers
{   
    [ApiController]
    [Route("api/as")]
    public class ControllerDeleteUser : ControllerBase
    {   
        private readonly Context _context;

        public ControllerDeleteUser (Context context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> PegarPessoapeloIdAsync(Guid Id){
            User? pessoa = await _context.Users.FindAsync (Id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> ExcluirPessoaAsync(Guid Id){
            User? User = await _context.Users.FindAsync(Id);

        if(User == null){
            return NotFound();
        }

            _context.Remove(User);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}