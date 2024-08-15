using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore_API.Models
{
    public class Order
    {

        [Key]
        public int order_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }

        public ICollection<OrderDetail>? orderdetails { get; set; }
    }
}
