using PetsMobile.Services.DTO;

namespace PetsMobile.Services.Interface;

public interface IRatingService
{
    Task<RatingDTO?> GetByIdAsync(long id);
}