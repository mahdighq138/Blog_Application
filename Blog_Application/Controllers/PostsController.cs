using Blog_Application.DB_Context;
using Blog_Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Dependency Injection
        private readonly BlogDbContext _blogContext;

        public PostsController(BlogDbContext blogContext)
        {
            _blogContext = blogContext;
        }

        #endregion

        #region Get All
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(List<BlogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var posts = _blogContext.Blogs?.ToList();

            if (posts != null || posts!.Count!=0)
            {
                return Ok(posts);
            }
            return NotFound();

        }
        #endregion


    }
}
