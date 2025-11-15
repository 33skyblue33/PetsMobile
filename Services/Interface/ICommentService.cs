using PetsMobile.Services.DTO;

namespace PetsMobile.Services.Interface
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(long petId, long userId, string text);
        Task<PaginatedResult<CommentDTO>> GetCommentsByPetIdAsync(long petId, int pageNumber, int pageSize);
    }
}
