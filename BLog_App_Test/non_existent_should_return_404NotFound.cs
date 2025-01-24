using Blog_Application.Controllers;
using Blog_Application.DB_Context;
using Blog_Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLog_App_Test
{
    public class non_existent_should_return_404NotFound
    {
        private readonly DbContextOptions<BlogDbContext> _dbContextOptions;
        public non_existent_should_return_404NotFound()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
        }

        [Fact]
        public void GetNonExistentPostShouldReturn404()
        {
            var mockLogger = new Mock<ILogger<PostsController>>();
            using var context = new BlogDbContext(_dbContextOptions);

            var controller = new PostsController(context, mockLogger.Object);

            int nonExistentId = 0;

            //act
            var result = controller.GetPost(nonExistentId);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

        }
    }
}

