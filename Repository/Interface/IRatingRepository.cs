using PetsMobile.Data;
using PetsMobile.Entities;

namespace PetsMobile.Repository.Interface;

public interface IRatingRepository
{
    Task<Rating?> GetByIdAsync(long id);
    Task<double> GetAverageRatingAsync(long petId);
    Task<Dictionary<long, double>> GetAverageRatingsAsync(List<Pet> pets);
    Task<PagedResult<Rating>> GetRatingsAsync(long petId, long? pointer, int pageSize);
    Task AddAsync(Rating rating);
}