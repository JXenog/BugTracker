using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models {
    public class Project : BaseEntity {
        [Required]
        public string? Title { get; set; }

        // Navigation Properties
        public virtual IList<Bug>? Bugs { get; set; }
    }
}
