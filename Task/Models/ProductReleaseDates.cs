using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Task.Models
{
    public class ProductReleaseDates
    {
        [Key,Required, ScaffoldColumn(false), Display(Name = "Id"), StringLength(40)]
        public string ProductId { get; set; }

        [Required]
        public DateTime RealeseOn { get; set; }
    }
}