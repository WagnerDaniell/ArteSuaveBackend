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
        /*
        [HttpPut("{Id}")]
        public async Task<ActionResult<User>> UpdateUser(Guid Id, [FromBody] User update){
            User? pessoa = await _context.Users.FindAsync (Id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);

        }
        */
    };
};