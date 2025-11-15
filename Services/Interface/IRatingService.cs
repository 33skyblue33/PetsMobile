using PetsMobile.Services.DTO;

namespace PetsMobile.Services.Interface
{
    public interface IRatingService
    {
        Task UpdatePetRating(long petId, int rating);
    }
}
