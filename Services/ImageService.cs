using PetsMobile.Services.Interface;

namespace PetsMobile.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env, ILogger<ImageService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return;
            }

            try
            {
                var fileName = Path.GetFileName(imageUrl);
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                if (!File.Exists(filePath))
                {
                    return;
                }

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting file for URL {imageUrl}: {ex.Message}");
            }
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, HttpContext context)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string imagesPath = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            string filePath = Path.Combine(imagesPath, uniqueFileName);

            using (FileStream fileStream = new(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            
            return $"/images/{uniqueFileName}";
        }
    }
}
