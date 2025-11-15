using Microsoft.EntityFrameworkCore;
using PetsMobile.Data;
using PetsMobile.Entities;
using PetsMobile.Repository.Interface;

namespace PetsMobile.Repository;

public class RatingRepository : IRatingRepository
{
    private readonly DatabaseContext _databaseContext;  
    
    public RatingRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Rating?> GetByIdAsync(long id)
    {
        return await _databaseContext.Ratings.FindAsync(id);
    }

    public async Task<double> GetAverageRatingAsync(long petId)
    {
        return await _databaseContext.Ratings.Where(r => r.PetId == petId).AverageAsync(r => r.Value);
    }

    public async Task<Dictionary<long, double>> GetAverageRatingsAsync(List<Pet> pets)
    {
        List<long> petIds = pets.Select(p => p.Id).ToList();
        
        return await _databaseContext.Ratings.Where(r => petIds.Contains(r.PetId)).GroupBy(r => r.PetId)
            .ToDictionaryAsync(g => g.Key, g => g.Average(r => r.Value));
    }

    public async Task<PagedResult<Rating>> GetRatingsAsync(long petId, long? pointer, int pageSize)
    {
        IQueryable<Rating> query = _databaseContext.Ratings.Where(r => r.PetId == petId).OrderByDescending(r => r.Id);

        if (pointer != null)
        {
            query = query.Where(r => r.Id < pointer.Value);
        }

        List<Rating> items = await query.Take(pageSize + 1).ToListAsync();
        bool hasNextPage = items.Count > pageSize;

        if (hasNextPage)
        {
            items.RemoveAt(items.Count - 1);
        }

        return new PagedResult<Rating>(items, items.LastOrDefault()?.Id, hasNextPage);
    }

    public async Task AddAsync(Rating rating)
    {
        await _databaseContext.Ratings.AddAsync(rating);
    }
}