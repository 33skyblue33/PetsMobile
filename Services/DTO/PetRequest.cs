namespace PetsMobile.Services.DTO;

public record PetRequest(string Name,string Color,int Age,IFormFile? Image,string Description, long BreedId);

