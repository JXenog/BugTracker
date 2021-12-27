using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
