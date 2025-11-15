using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;

namespace PetsMobile.Controllers
{
    [Route("api/v3/pets/{petId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentDTO>> AddComment(long petId, [FromBody] CommentRequestDTO commentRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var comment = await _commentService.AddCommentAsync(petId, long.Parse(userId), commentRequest.Text);
                return CreatedAtAction(nameof(GetComments), new { petId = petId }, comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<CommentDTO>>> GetComments(long petId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _commentService.GetCommentsByPetIdAsync(petId, pageNumber, pageSize));
        }
    }
}
