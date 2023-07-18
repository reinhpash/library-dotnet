using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models
{
    public class BookType
    {
        [Key] public int genreID { get; set; } //Key primary key oluşturmamızı sağlıyor.

        [Required]
        [MaxLength(25)]
        [DisplayName("Genre")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "The genre name cannot be empty, and it cannot contain special characters or numbers.")]
        public string type { get; set; } //Required Null olamayacağını belirtiyor
    }
}
