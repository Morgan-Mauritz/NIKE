namespace NikeClientApp.Models
{
    public class LikeDislikeEntry
    {
        public long Id { get; set; }
        public long EntryId { get; set; }
        public long Likes { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Entry Entry { get; set; }
    }
}
