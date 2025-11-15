namespace PetsMobile.Data;

public record PagedResult<T>(List<T> Items, long? NextPointer, bool HasNextPage);