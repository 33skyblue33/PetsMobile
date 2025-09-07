using PetsMobile.Repository.Interface;
using PetsMobile.Services.DTO;
using PetsMobile.Services.Interface;

namespace PetsMobile.Services
{
    public class BreedService : IBreedService
    {
        private readonly IBreedRepository _breedRepository;

        public  BreedService(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<PetDTO> CreateAsync(BreedRequest data)
        {
            throw new NotImplementedException();
        
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BreedDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BreedDTO?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long id, BreedRequest data)
        {
            throw new NotImplementedException();
        }
    }
}
