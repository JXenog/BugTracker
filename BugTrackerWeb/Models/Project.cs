using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Priority")]
        [Range(1, 100, ErrorMessage = "Priority must be between 1 and 100!!")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
