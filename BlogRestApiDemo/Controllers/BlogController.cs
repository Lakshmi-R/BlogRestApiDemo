using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Models;

namespace BlogRestApiDemo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public BlogController(IPostRepository repository)
        {
            postRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await postRepository.GetAllPostAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await postRepository.GetPostByIdAsync(id);

            if (result == null) return NotFound(ApiResponse<string>.FailureResponse("post not found"));
            return Ok(ApiResponse<Post>.SuccessResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            var result = await postRepository.CreatePostAysnc(post);

            if (result == null) return NotFound(ApiResponse<string>.FailureResponse("Error in adding post"));
            return Ok(ApiResponse<Post>.SuccessResponse(result));
        }
    }
}
