using Microsoft.EntityFrameworkCore;
using PizzaStore_API.Models;
using PizzaStore_API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaStoreDb")));
builder.Services.AddScoped<CsvImportService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var importService = services.GetRequiredService<CsvImportService>();

    importService.ImportOrders("CSVFiles/orders.csv");
    importService.ImportPizzaTypes("CSVFiles/pizza_types.csv");
    importService.ImportPizzas("CSVFiles/pizzas.csv");
    importService.ImportOrderDetails("CSVFiles/order_details.csv");

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
