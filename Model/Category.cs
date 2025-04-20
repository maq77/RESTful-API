using System.ComponentModel.DataAnnotations;

namespace RESTful_API.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}
