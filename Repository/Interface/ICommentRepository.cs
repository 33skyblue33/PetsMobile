using PetsMobile.Entities;

namespace PetsMobile.Repository.Interface
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetByPetIdAsync(long petId, int pageNumber, int pageSize);
        Task<int> GetCountByPetIdAsync(long petId);
    }
}
