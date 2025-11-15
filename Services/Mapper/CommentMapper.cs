namespace PetsMobile.Services.Mapper;

using DTO;
using Entities;

public static class CommentMapper {
  public static CommentDTO CommentToCommentDTO(Comment comment) {
    return new CommentDTO(
      comment.Id,
      comment.Text,
      comment.PetId,
      comment.UserId
    );
  }
}
