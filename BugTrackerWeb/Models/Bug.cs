using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models {
    public class Bug : BaseEntity {
        public enum SeverityTypes {
            [Display(Name = "Low")] Low,
            [Display(Name = "Moderate")] Moderate,
            [Display(Name = "Major")] Major,
            [Display(Name = "Critical")] Critical
        }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }
        public bool Fixed { get; set; } = false;
        [Required]
        public SeverityTypes Severity { get; set; }

        // Navigation Properties
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
