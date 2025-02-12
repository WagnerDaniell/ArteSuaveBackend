using ASbackend.Models;
using Microsoft.AspNetCore.Mvc;
using ASbackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ASbackend.Controllers
{
    [ApiController]
    [Route("api/as")]
    public class ControllerLogin : ControllerBase
    {        
        private readonly Context _context;

        public ControllerLogin(Context context){

            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> RegisterUsers(User user){

            var ExistingUser = await _context.Users.FirstOrDefaultAsync(u=> u.Email == user.Email); 

            if(ExistingUser != null){
                return BadRequest(new{message = "Email já utilizado!"});
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = TokenService.GenerateToken(user);

            return Created("api/as/register",new
            {
                token = token
            });
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
    };
};