using Microsoft.EntityFrameworkCore;

namespace ASbackend.Models
{
    public class Context : DbContext
    {
        public required DbSet<User> Users {get; set;}

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}