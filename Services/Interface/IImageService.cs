namespace PetsMobile.Services.Interface
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, HttpContext context);
        void DeleteImage(string imageUrl);
    } 
}
