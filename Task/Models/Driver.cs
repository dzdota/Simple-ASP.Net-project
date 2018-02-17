using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Models
{
    public class Driver
    {
        [Key, Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        [StringLength(20)]
        public string Version { get; set; }

        public long Size { get; set; }

        [Required ,StringLength(100)]
        public string CompanyName { get; set; }

        [Required, StringLength(100)]
        public string ProductCategory { get; set; }
    }
}