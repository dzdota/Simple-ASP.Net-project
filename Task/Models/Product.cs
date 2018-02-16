using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Task.Models
{
    public class Product
    {
        [Required, ScaffoldColumn(false), StringLength(40)]
        public string ProductId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]   
        public string ProductName { get; set; }

        [StringLength(200), Display(Name = "Url")]
        public string Url { get; set; }

        [StringLength(20), Display(Name = "Contact")]
        public string VendorContact { get; set; }

    }
}