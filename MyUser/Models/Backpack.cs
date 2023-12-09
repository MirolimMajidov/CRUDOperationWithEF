namespace MyUser.Models
{
    public class Backpack : BaseEntity
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
