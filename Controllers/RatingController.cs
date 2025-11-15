using Microsoft.AspNetCore.Mvc;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;

namespace PetsMobile.Controllers;

[Route("api/v3/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }
    
    [HttpGet("{id}", Name = "GetRatingById")]
    public async Task<ActionResult<RatingDTO>> GetRating(long id)
    {
        RatingDTO? rating = await _ratingService.GetByIdAsync(id);
        return rating != null ? Ok(rating) : NotFound();
    }
}