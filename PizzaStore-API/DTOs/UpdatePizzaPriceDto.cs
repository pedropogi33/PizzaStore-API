namespace PizzaStore_API.DTOs
{
    public class UpdatePizzaPriceDto
    {
        public string PizzaId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
