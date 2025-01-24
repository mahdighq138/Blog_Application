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
        private readonly ILogger<PostsController> _logger;

        public PostsController(BlogDbContext blogContext, ILogger<PostsController> logger)
        {
            _blogContext = blogContext;
            _logger = logger;
        }

        #endregion

        #region Get All
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(List<BlogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            try
            {
                var posts = _blogContext.Blogs?.ToList();
                if (posts != null && posts!.Count != 0)
                {
                    _logger.LogInformation("All posts returned. Total Number:{p0}", posts.Count);
                    return Ok(posts);
                }
                _logger.LogInformation("No Posts Found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Message:{p0}", ex.Message);
                return NotFound();
            }

            

        }
        #endregion

        #region Get Post
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BlogDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = _blogContext.Blogs?.Find(id);

                if (post != null)
                {
                    _logger.LogInformation("The Post Found");
                    return Ok(post);
                }
                _logger.LogInformation("No such Post");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Message:{p0}", ex.Message);
                return NotFound();
            }
        }
        #endregion

        #region Delete Post
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePost(int id)
        {
            try
            {
                var post = _blogContext.Blogs?.Find(id);
                if (post != null)
                {
                    _blogContext.Blogs!.Remove(post);
                    _blogContext.SaveChanges();
                    _logger.LogInformation("The Post Deleted");
                    return Ok();
                }
                _logger.LogInformation("No such Post");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Message:{p0}", ex.Message);
                return NotFound();
            }
        }
        #endregion

        #region Update Post
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePost(int id, BlogDTO blog)
        {
            try
            {
                var post = _blogContext.Blogs?.Find(id);
                if (post == null)
                {
                    _logger.LogInformation("No such Post");
                    return NotFound();
                }

                post.Title = blog.Title;
                post.Contents = blog.Contents;
                post.LastChangeDate = DateTime.Now;

                _blogContext.Blogs!.Update(post);

                _blogContext.Update(blog);
                _blogContext.SaveChanges();
                _logger.LogInformation("The Post Updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Message:{p0}", ex.Message);
                return NotFound();
            }
        }
        #endregion

        #region Insert Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult InsertPost(BlogDTO blog)
        {
            try
            {
                blog.LastChangeDate = DateTime.Now;
                _blogContext.Blogs!.Add(blog);
                _blogContext.SaveChanges();
                _logger.LogInformation("The Post added");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Message:{p0}", ex.Message);
                return NotFound();
            }
        }
        #endregion

    }
}
