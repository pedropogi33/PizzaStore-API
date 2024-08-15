using Microsoft.EntityFrameworkCore;

namespace PizzaStore_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
              ChangeTracker.LazyLoadingEnabled = false;
            this.Database.SetCommandTimeout(0);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.order_id);
            modelBuilder.Entity<Order>()
                .Property(o => o.order_id)
                .ValueGeneratedNever();  

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.order_details_id);
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.order_details_id)
                .ValueGeneratedNever(); 

            modelBuilder.Entity<Pizza>()
                .HasKey(p => p.pizza_id);
            modelBuilder.Entity<Pizza>()
                .Property(p => p.pizza_id)
                .ValueGeneratedNever();  

            modelBuilder.Entity<PizzaType>()
                .HasKey(pt => pt.pizza_type_id);
            modelBuilder.Entity<PizzaType>()
                 .Property(p => p.pizza_type_id)
                 .ValueGeneratedNever();  

            modelBuilder.Entity<Order>()
                .HasMany(o => o.orderdetails)
                .WithOne(od => od.order)
                .HasForeignKey(od => od.order_id);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.pizza)
                .WithMany()
                .HasForeignKey(od => od.pizza_id);

            modelBuilder.Entity<Pizza>()
                .HasOne(p => p.pizzatype)
                .WithMany(pt => pt.pizzas);


        }
    }
}
