namespace PetsMobile.Services.DTO;

public record PaginatedResult<T>(int PageNumber, int PageSize, int TotalPages, int TotalRecords, List<T> Data);
