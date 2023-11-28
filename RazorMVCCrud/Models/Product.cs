using System.ComponentModel.DataAnnotations;

namespace RazorMVCCrud.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"),
            Display(Name ="Product Name"),StringLength(100)]

        public string? Name { get; set; }

        [Required(ErrorMessage ="Price is required"),Range(0,Double.PositiveInfinity)]
        public double? Price { get; set; }
        public DateTime? CreationDate { get; set; }=DateTime.Now;

    }
}
