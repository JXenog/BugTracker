using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models
{
    public class Project : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Display(Name = "Priority")]
        [Range(1, 100, ErrorMessage = "Priority must be between 1 and 100!!")]
        public int DisplayOrder { get; set; }
    }
}
