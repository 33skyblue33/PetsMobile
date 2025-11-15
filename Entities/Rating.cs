namespace PetsMobile.Entities;

public class Rating
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PetId { get; set; }
    public int Value { get; set; }
    public string Comment { get; set; }
    
    public User User { get; set; }
}