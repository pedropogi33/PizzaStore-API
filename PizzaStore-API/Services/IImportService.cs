namespace PizzaStore_API.Services
{
    public interface IImportService
    {
        void ImportOrders(string filePath);
        void ImportOrderDetails(string filePath);
        void ImportPizzas(string filePath);
        void ImportPizzaTypes(string filePath);
    }
}
