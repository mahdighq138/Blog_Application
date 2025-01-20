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

            if (posts != null && posts!.Count != 0)
            {
                return Ok(posts);
            }
            return NotFound();

        }
        #endregion

        #region Get Post
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BlogDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int id)
        {
            var post = _blogContext.Blogs?.Find(id);

            if (post != null)
            {
                return Ok(post);
            }
            return NotFound();

        }
        #endregion

        #region Delete Post
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePost(int id)
        {
            var post = _blogContext.Blogs?.Find(id);
            if (post != null)
            {
                _blogContext.Blogs!.Remove(post);
                _blogContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        #endregion

        #region Update Post
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePost(int id, BlogDTO blog)
        {
            var post = _blogContext.Blogs?.Find(id);
            if (post == null)
            {
               return NotFound();
            }

            post.Title = blog.Title;
            post.Contents = blog.Contents;
            post.LastChangeDate = DateTime.Now;

            _blogContext.Blogs!.Update(post);
            _blogContext.SaveChanges();
            return Ok();
        }
        #endregion

        #region Insert Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult InsertPost(BlogDTO blog)
        {
            blog.LastChangeDate = DateTime.Now;
            _blogContext.Blogs!.Add(blog);
            _blogContext.SaveChanges();
            return Ok();
        }
        #endregion

    }
}
