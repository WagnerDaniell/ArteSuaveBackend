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