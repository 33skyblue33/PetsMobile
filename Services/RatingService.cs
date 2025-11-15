using PetsMobile.Entities;
using PetsMobile.Repository.Interface;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;
using PetsMobile.Services.Mapper;

namespace PetsMobile.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }
    
    public async Task<RatingDTO?> GetByIdAsync(long id)
    {
        Rating? rating = await _ratingRepository.GetByIdAsync(id);
        return rating != null ? RatingMapper.RatingToRatingDTO(rating) : null;
    }
}