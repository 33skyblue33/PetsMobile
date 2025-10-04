using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;

namespace PetsMobile.Controllers
{
    [Route("api/v3/[Controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IImageService _imageService;

        public PetsController(IPetService petService, IImageService imageService)
        {
            _petService = petService;
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetById(long id)
        {
            PetDTO? pet = await _petService.GetByIdAsync(id);

            return pet != null ? Ok(pet) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<PetDTO>>> GetAll()
        {
            return Ok(await _petService.GetAllAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> Create([FromForm] PetRequest data)
        {
            if (data.Image == null || data.Image.Length == 0)
            {
                return BadRequest("An image file is required.");
            }

            string imageUrl = await _imageService.SaveImageAsync(data.Image, HttpContext);
            PetDTO pet = await _petService.CreateAsync(data, imageUrl);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> Update(long id, [FromForm] PetRequest data)
        {
            string? imageUrl = data.Image != null && data.Image.Length != 0 ? 
                await _imageService.SaveImageAsync(data.Image, HttpContext)
                : null;

            return await _petService.UpdateAsync(id, data, imageUrl) ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> Delete(long id)
        {
            string? imageUrl = await _petService.DeleteAsync(id);

            if (imageUrl == null)
            {
                return NotFound();
            }

            _imageService.DeleteImage(imageUrl);
            return NoContent();
        }
    }
}
