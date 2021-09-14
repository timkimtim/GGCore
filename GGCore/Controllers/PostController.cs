using AutoMapper;
using GGCore.DTOs;
using GGCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Controllers
{
    [ApiController]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _unitOfWork.Posts.GetAll();
                var result = _mapper.Map<IList<PostDTO>>(posts);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Smth went wrong in the {nameof(GetPosts)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _unitOfWork.Posts.Get(q => q.Id == id, new List<string> { "Comments" });
                var result = _mapper.Map<PostDTO>(post);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Smth went wrong in the {nameof(GetPost)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
    }
}
