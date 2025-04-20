using System.ComponentModel.DataAnnotations;

namespace RESTful_API.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<OrderItem>? ordersItems { get; set; }
    }
}
