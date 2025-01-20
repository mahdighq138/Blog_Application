using System.ComponentModel.DataAnnotations;

namespace Blog_Application.DTO
{
    public class BlogDTO
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Contents { get; set; }
        public DateTime? LastChangeDate { get; set; }
    }
}
