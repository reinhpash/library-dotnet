using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models
{
    public class Book
    {
        [Key]public int Id { get; set; }
        [Required] public string Title { get; set; }
        public string Description { get; set; }
        [Required]public string Author { get; set; }
        [Required][Range(10,5000)]public double price { get; set; }
    }
}
