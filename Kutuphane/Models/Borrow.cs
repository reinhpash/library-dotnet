using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kutuphane.Models
{
    public class Borrow
    {
        [Key]public int ID { get; set; }

        [DisplayName("First Name")][Required]public string FirstName { get; set; }
        [DisplayName("Last Name")][Required]public string LastName { get; set; }


        [ValidateNever]
        [ForeignKey("Book")]
        public int bookID { get; set; }
        [ValidateNever]
        public virtual Book Book { get; set; }



    }
}
