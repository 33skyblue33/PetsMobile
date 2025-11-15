namespace PetsMobile.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long PetId { get; set; }
        public Pet Pet { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
