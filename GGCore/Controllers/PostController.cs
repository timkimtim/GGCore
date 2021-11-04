using AutoMapper;
using GGCore.DTOs;
using GGCore.Models;
using GGCore.Repositories;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Controllers
{
    //[ApiVersion("2.0")]
    [ApiController]
    //[Route("api/v{v:apiversion}/posts")]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;

        public PostController(IUnitOfWork unitOfWork, ILogger<PostController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        //[ResponseCache(CacheProfileName = "30SecondsDuration")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] // overwrite default in Startup.cs
        [HttpCacheValidation(MustRevalidate = false)] // overwrite default in Startup.cs
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPosts([FromQuery] RequestParams requestParams)
        {
            var posts = await _unitOfWork.Posts.GetPagedList(requestParams);
            var result = _mapper.Map<IList<PostDTO>>(posts);
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id, new List<string> { "Comments" });
            var result = _mapper.Map<PostDTO>(post);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO postDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreatePost)}");
                return BadRequest(ModelState);
            }

            var post = _mapper.Map<Post>(postDTO);
            await _unitOfWork.Posts.Insert(post);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetPost", new { id = post.Id }, post);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDTO postDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(UpdatePost)}");
                return BadRequest(ModelState);
            }

            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            if (post == null)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(UpdatePost)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(postDTO, post);
            _unitOfWork.Posts.Update(post);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(UpdatePost)}");
                return BadRequest(ModelState);
            }

            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            if (post == null)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(DeletePost)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Posts.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
