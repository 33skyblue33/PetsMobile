using PetsMobile.Entities;
using PetsMobile.Repository.Interface;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;
using PetsMobile.Services.Mapper;

namespace PetsMobile.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDTO> AddCommentAsync(long petId, long userId, string text)
        {
            var pet = await _unitOfWork.PetRepository.GetByIdAsync(petId);
            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            var comment = new Comment
            {
                PetId = petId,
                UserId = userId,
                Text = text
            };

            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return CommentMapper.CommentToCommentDTO(comment);
        }

        public async Task<PaginatedResult<CommentDTO>> GetCommentsByPetIdAsync(long petId, int pageNumber, int pageSize)
        {
            var comments = await _unitOfWork.CommentRepository.GetByPetIdAsync(petId, pageNumber, pageSize);
            var totalRecords = await _unitOfWork.CommentRepository.GetCountByPetIdAsync(petId);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var commentDTOs = comments.Select(CommentMapper.CommentToCommentDTO).ToList();

            return new PaginatedResult<CommentDTO>(pageNumber, pageSize, totalPages, totalRecords, commentDTOs);
        }
    }
}
