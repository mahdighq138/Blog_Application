using Blog_Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace Blog_Application.DB_Context
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options) { }
        public DbSet<BlogDTO> Blogs { get; set; }
    }
}
