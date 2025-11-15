using PetsMobile.Repository.Interface;
using PetsMobile.Services.Interface;

namespace PetsMobile.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdatePetRating(long petId, int rating)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            var pet = await _unitOfWork.PetRepository.GetById(petId);
            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            pet.Rating = rating;
            await _unitOfWork.PetRepository.Update(pet);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
