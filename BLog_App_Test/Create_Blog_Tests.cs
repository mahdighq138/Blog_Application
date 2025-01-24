using Blog_Application.Controllers;
using Blog_Application.DB_Context;
using Blog_Application.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
namespace BLog_App_Test
{
    public class Create_Blog_Tests
    {
        private readonly DbContextOptions<BlogDbContext> _dbContextOptions;
        public Create_Blog_Tests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
        }

        [Fact]
        public void CreateBlogSuccessfully()
        {
            var mockLogger = new Mock<ILogger<PostsController>>();
            using var context = new BlogDbContext(_dbContextOptions);

            var controller= new PostsController(context, mockLogger.Object);

            var newBlog = new BlogDTO
            {
                Title = "Test Blog",
                Contents = "This is a test blog post."
            };

            //act
            controller.InsertPost(newBlog);


            var savedBlog =  context.Blogs.FirstOrDefault();
            Assert.NotNull(savedBlog);
            Assert.Equal(newBlog.Title, savedBlog.Title);
            Assert.Equal(newBlog.Contents, savedBlog.Contents);

        }
    }
}