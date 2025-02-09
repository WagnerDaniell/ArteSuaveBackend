using ASbackend.Models;
using Microsoft.AspNetCore.Mvc;
using ASbackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ASbackend.Controllers
{
    [ApiController]
    [Route("api/as")]
    public class ControllerRegister : ControllerBase
    {
        private readonly Context _context;

        public ControllerRegister (Context context)
        {
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
    };
};