using PetsMobile.Data;
using PetsMobile.Services.Exceptions;

namespace PetsMobile.Services;

using DTO;
using Entities;
using Interface;
using Mapper;
using Repository.Interface;

public class PetService : IPetService {
  private readonly IPetRepository _petRepository;
  private readonly IBreedRepository _breedRepository;
  private readonly IRatingRepository _ratingRepository;
  private readonly IUnitOfWork _unitOfWork;
  
  public PetService(IPetRepository petRepository,
    IBreedRepository breedRepository, IRatingRepository ratingRepository,
    IUnitOfWork unitOfWork) {
    _petRepository = petRepository;
    _breedRepository = breedRepository;
    _ratingRepository = ratingRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<PetDTO?> GetByIdAsync(long id) {
    Pet? pet = await _petRepository.GetByIdAsync(id);
    
    if (pet == null) {
      return null;
    }

    double averageRating = await _ratingRepository.GetAverageRatingAsync(pet.Id);
    return PetMapper.PetToPetDTO(pet, averageRating);
  }
  
  public async Task<List<PetDTO>> GetAllAsync()
  {
    List<Pet> pets = await _petRepository.GetAllAsync();
    Dictionary<long, double> averageRatings = await _ratingRepository.GetAverageRatingsAsync(pets);
    
    return PetMapper.PetListToPetDTOList(pets, averageRatings);
  }
  
  public async Task<PetDTO> CreateAsync(PetRequest data, string imageUrl) {
    Breed? breed = await _breedRepository.GetByIdAsync(data.BreedId);
    Pet pet = PetMapper.PetRequestToPet(data, breed, imageUrl);
    
    await _petRepository.AddAsync(pet);
    await _unitOfWork.CompleteAsync();
    
    return PetMapper.PetToPetDTO(pet, 0.0);
  }
  
  public async Task<bool> UpdateAsync(long id,
    PetRequest data, string? imageUrl) {
    Pet? pet = await _petRepository.GetByIdAsync(id);
    Breed? breed = await _breedRepository.GetByIdAsync(data.BreedId);

    if(pet == null || breed == null)
    {
        return false; 
    }
    
    PetMapper.MapPet(pet, breed, data, imageUrl);
    
    return await _unitOfWork.CompleteAsync() != 0;
  }
  public async Task<string?> DeleteAsync(long id) {
    Pet? pet = await _petRepository.GetByIdAsync(id);

    if(pet == null)
    {
       return null;
    }
    
    _petRepository.Remove(pet);
    return await _unitOfWork.CompleteAsync() != 0 ? pet.ImageUrl : null;
  }

  public async Task<PagedResult<RatingDTO>> GetRatingsAsync(long id, long? pointer, int pageSize)
  {
    return RatingMapper.RatingPagedResultToRatingDTOPagedResult(
      await _ratingRepository.GetRatingsAsync(id, pointer, pageSize));
  }

  public async Task<RatingDTO?> AddRatingAsync(long id, long userId, RatingRequest data)
  {
    if (data.Value < 1 || data.Value > 5)
    {
      throw new InvalidRatingException("Ratings must be between 1 and 5.");
    }

    Rating rating = RatingMapper.RatingRequestToRating(id, userId, data);
    await _ratingRepository.AddAsync(rating);
    
    return await _unitOfWork.CompleteAsync() != 0 ? RatingMapper.RatingToRatingDTO(rating) : null;
  }
}
