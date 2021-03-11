using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
