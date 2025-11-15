namespace PetsMobile.Services.Mapper;

using DTO;
using Entities;

public static class PetMapper {
  public static PetDTO PetToPetDTO(Pet pet, double averageRating) {
    return new PetDTO(
      pet.Id,
      pet.Name,
      pet.Color,
      pet.Age,
      pet.ImageUrl,
      pet.Description,
      averageRating,
      pet.Breed.Name, 
      pet.Breed.Description
    );
  }
  
  public static List<PetDTO> PetListToPetDTOList(List<Pet> pets, Dictionary<long, double> averageRatings) {
    return pets.Select(p => PetToPetDTO(p, averageRatings.TryGetValue(p.Id, out double rating) ? rating : 0.0)).ToList();
  }
  
  public static Pet PetRequestToPet(PetRequest petRequest,
    Breed breed, string imageUrl) {
    return new Pet() {
      Name = petRequest.Name,
      Color = petRequest.Color,
      Age = petRequest.Age,
      ImageUrl = imageUrl,
      Description = petRequest.Description,
      Breed = breed
    };
  }
  public static void MapPet(Pet pet,
    Breed breed,
    PetRequest data, string? imageUrl) {
    
    pet.Name = data.Name;
    pet.Color = data.Color;
    pet.Age = data.Age;
    pet.ImageUrl = imageUrl != null ? imageUrl : pet.ImageUrl;
    pet.Description = data.Description;
    pet.Breed = breed;
  }
}
