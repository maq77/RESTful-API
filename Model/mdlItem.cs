using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RESTful_API.Model
{
    public class mdlItem
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public double Price { get; set; }
        [MaxLength(50)]
        public string? Notes { get; set; }
        public IFormFile? Image { get; set; } /// this for uploading file into mdl and transfer it into actual itme.
        public int CategoryId { get; set; }
    }
}
