using Microsoft.EntityFrameworkCore;
using PetsMobile.Data;
using PetsMobile.Entities;
using PetsMobile.Repository.Interface;

namespace PetsMobile.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CommentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddAsync(Comment comment)
        {
            await _databaseContext.Comments.AddAsync(comment);
        }

        public async Task<List<Comment>> GetByPetIdAsync(long petId, int pageNumber, int pageSize)
        {
            return await _databaseContext.Comments
                .Where(c => c.PetId == petId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountByPetIdAsync(long petId)
        {
            return await _databaseContext.Comments.CountAsync(c => c.PetId == petId);
        }
    }
}
