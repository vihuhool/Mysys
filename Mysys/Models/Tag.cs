namespace Mysys.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ItemId { get; set; }
        public int CollectionId { get; set; }
    }

}
