using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore_API.Models
{
    public class PizzaType
    {
        [Key]
        public string pizza_type_id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string ingredients { get; set; }

        public ICollection<Pizza>? pizzas { get; set; }
    }
}
