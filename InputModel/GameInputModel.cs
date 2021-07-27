using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Game´s Title must contain between 3 and 100 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Publisher's Name must contain between 3 and 100 characters")]
        public string Publisher { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Price must have a $1 minimum and a $1000 maximum")]
        public double Price { get; set; }
    }
}
