﻿using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models
{
    public class Bug : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public bool Fixed { get; set; } = false;
    }
}