using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class ProductReleaseDates
    {
        [Required, ScaffoldColumn(false), Display(Name = "Id"), StringLength(40)]
        public string ProductId { get; set; }

        [Required, StringLength(30)]
        public string RealeseOn { get; set; }
    }
}