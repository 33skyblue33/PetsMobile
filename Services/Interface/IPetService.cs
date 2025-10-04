namespace PetsMobile.Services.Interface
{
  using DTO;

  public interface IPetService {
    Task<PetDTO?> GetByIdAsync(long id);
    Task<List<PetDTO>> GetAllAsync();
    Task<PetDTO> CreateAsync(PetRequest data, string imageUrl);
    Task<bool> UpdateAsync(long id, PetRequest data, string? imageUrl);
    //Returns image url of the deleted pet
    Task<string?> DeleteAsync(long id);
  }
}
