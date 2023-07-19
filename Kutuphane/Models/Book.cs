using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kutuphane.Models
{
    public class Book
    {
        [Key]public int ID { get; set; }
        [Required] public string Title { get; set; }
        public string Description { get; set; }
        [Required]public string Author { get; set; }
        [Required][Range(10,5000)]public double price { get; set; }

        [ValidateNever]
        public int genreID { get; set; }
        [ForeignKey("genreID")]

        [ValidateNever]
        public BookType bookType { get; set; }

        [ValidateNever]
        public string imageUrl { get; set; }
    }
}
