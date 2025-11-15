using PetsMobile.Data;
using PetsMobile.Entities;
using PetsMobile.Services.DTO;

namespace PetsMobile.Services.Mapper;

public static class RatingMapper
{
    public static PagedResult<RatingDTO> RatingPagedResultToRatingDTOPagedResult(PagedResult<Rating> ratings)
    {
        return new PagedResult<RatingDTO>(ratings.Items.Select(RatingToRatingDTO).ToList(), ratings.NextPointer,
            ratings.HasNextPage);
    }

    public static Rating RatingRequestToRating(long petId, long userId, RatingRequest request)
    {
        return new Rating()
        {
            PetId = petId,
            UserId = userId,
            Value = request.Value,
            Comment = request.Comment
        };
    }

    public static RatingDTO RatingToRatingDTO(Rating rating)
    {
        return new RatingDTO(rating.Id, rating.UserId, rating.Value, rating.Comment);
    }
}