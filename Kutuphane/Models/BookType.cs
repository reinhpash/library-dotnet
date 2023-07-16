using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models
{
    public class BookType
    {
        [Key] public int ID { get; set; } //Key primary key oluşturmamızı sağlıyor.
        [Required] [DisplayName("Genre")]public string type { get; set; } //Required Null olamayacağını belirtiyor
    }
}
