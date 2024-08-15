using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore_API.Models
{
    public class OrderDetail
    {
        [Key]
        public int order_details_id { get; set; }
        public int order_id { get; set; }
        public string pizza_id { get; set; }
        public int quantity { get; set; }
        public Order? order { get; set; }
        public Pizza? pizza { get; set; }
    }
}
