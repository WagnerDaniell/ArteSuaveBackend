using ASbackend.Models;
using ASbackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASbackend.Controllers
{
    [ApiController]
    [Route("api/as")]
    public class ASController : ControllerBase
    {
        private readonly Context _context;

        public ASController(Context context)
        {
            _context = context;
        } 

        [HttpGet("gerenciar")]
        public async Task<ActionResult<IEnumerable<User>>> Viewallusers(){
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> PegarPessoapeloIdAsync(Guid Id){
            User? pessoa = await _context.Users.FindAsync (Id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);

        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<User>> RegisterUsers(User user){

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("login")]

        public async Task<ActionResult<dynamic>> Authenticated([FromBody] UserLogin model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.Email.ToLower().Trim() == model.Email);

            if (user == null)
            {
                return NotFound(new{message = "Email not found!"});
            };

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (isPasswordValid == false)
            {
                return NotFound(new{message = "Password incorrect!"});
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return Ok(new
            {
                token = token
            });
            
        }

        //Daqui pra baixo não ta pronto!
        
        [HttpPut]
        public async Task<ActionResult> AtualizarPessoaAsync(User user){
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
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