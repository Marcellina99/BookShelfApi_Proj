using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookSelfApi
{
    public class Book
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Invalid Description")]
        public string? Description { get; set; }
        
    }
}