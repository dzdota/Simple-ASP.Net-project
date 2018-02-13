using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Driver
    {
        [Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required, StringLength(20)]
        public string Version { get; set; }

        [Required]
        public int Size { get; set; }

        [Required ,StringLength(100)]
        public string CompanyName { get; set; }

        [Required, StringLength(100)]
        public string ProductCategory { get; set; }
    }
}