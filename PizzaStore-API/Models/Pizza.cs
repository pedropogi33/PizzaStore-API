using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore_API.Models
{
    public class Pizza
    {
        [Key]
        public string pizza_id { get; set; }
        public string? pizza_type_id { get; set; }
        public string size { get; set; }
        public decimal price { get; set; }
        public PizzaType? pizzatype { get; set; }
    }
}
