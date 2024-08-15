using CsvHelper;
using CsvHelper.Configuration;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PizzaStore_API.Models;
using PizzaStore_API.Services;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

public class CsvImportService
{
    private readonly IServiceProvider _serviceProvider;
    public CsvImportService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ImportOrders(string filePath)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.SetCommandTimeout(0);

                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null
                    }))
                    {
                        if (!context.Orders.Any())
                        {
                            var records = csv.GetRecords<Order>().ToList();
                            var data = records
                                                   .Select(p => new Order
                                                   {
                                                       order_id = p.order_id,
                                                       date = p.date,
                                                       time = p.time,
                                                   })
                                                    .ToList();

                            context.Database.SetCommandTimeout(500);
                            context.BulkInsert(data);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }
                
            }
        }
    }

    public void ImportOrderDetails(string filePath)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.ChangeTracker.AutoDetectChangesEnabled = false;

                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null
                    }))
                    {
                        if (!context.OrderDetails.Any())
                        {
                            var records = csv.GetRecords<OrderDetail>().ToList();
                            var data = records.Select(p => new OrderDetail
                            {
                                order_details_id = p.order_details_id,
                                order_id = p.order_id,
                                pizza_id = p.pizza_id,
                                quantity = p.quantity,
                            }).ToList();

                            int batchSize = 100;
                            int totalRecords = data.Count;

                            for (int i = 0; i < totalRecords; i += batchSize)
                            {
                                var batch = data.Skip(i).Take(batchSize).ToList();
                                context.Database.SetCommandTimeout(500);
                                context.BulkInsert(batch, new BulkConfig
                                {
                                    PreserveInsertOrder = true,
                                    SetOutputIdentity = true,
                                    UseTempDB = true,
                                });
                            }
                        }
                    }

                    context.ChangeTracker.AutoDetectChangesEnabled = true;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }


    public void ImportPizzas(string filePath)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null
                    }))
                    {
                        if (!context.Pizzas.Any())
                        {
                            var records = csv.GetRecords<Pizza>().ToList();
                            var data = records
                                                   .Select(p => new Pizza
                                                   {
                                                       pizza_id = p.pizza_id,
                                                       pizza_type_id = p.pizza_type_id,
                                                       size = p.size,
                                                       price = p.price,
                                                   }).Distinct()
                                                    .ToList();

                            context.Database.SetCommandTimeout(500);
                            context.BulkInsert(records, new BulkConfig
                            {
                                PreserveInsertOrder = true,
                                SetOutputIdentity = true,
                                UseTempDB = true,
                            });
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                
            }
        }
    }


    public void ImportPizzaTypes(string filePath)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null
                    }))
                    {
                        if (!context.PizzaTypes.Any())
                        {
                            var records = csv.GetRecords<PizzaType>().ToList();
                            var data = records
                                                   .Select(p => new PizzaType
                                                   {
                                                       pizza_type_id = p.pizza_type_id,
                                                       name = p.name,
                                                       category = p.category,
                                                       ingredients = p.ingredients,
                                                   })
                                                    .ToList();

                            context.Database.SetCommandTimeout(500);
                            context.BulkInsert(records, new BulkConfig
                            {
                                PreserveInsertOrder = true,
                                SetOutputIdentity = true,
                                UseTempDB = true,
                            });
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }
            }
        }
    }
}
