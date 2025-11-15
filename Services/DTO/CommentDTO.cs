namespace PetsMobile.Services.DTO;

public record CommentDTO(long Id, string Text, long PetId, long UserId);
