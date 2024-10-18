using CarStore_MinimalAPI;
using Microsoft.EntityFrameworkCore;

class CarDB(DbContextOptions<CarDB> options) : DbContext(options)
{
    public DbSet<CarModel> Cars => Set<CarModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CarModel>()
            .HasData(
            new CarModel { Id = 1, Brand = "Toyota", Model = "Corolla", Color = "White", Owner = "John Doe", Price = 15000, IsAvailable = true },
            new CarModel { Id = 2, Brand = "Honda", Model = "Civic", Color = "Black", Owner = "Jane Doe", Price = 20000, IsAvailable = false },
            new CarModel { Id = 3, Brand = "Ford", Model = "Fusion", Color = "Red", Owner = "Sam Smith", Price = 25000, IsAvailable = true },
            new CarModel { Id = 4, Brand = "Chevrolet", Model = "Cruze", Color = "Blue", Owner = "John Doe", Price = 15000, IsAvailable = true },
            new CarModel { Id = 5, Brand = "Nissan", Model = "Sentra", Color = "Black", Owner = "Jane Doe", Price = 20000, IsAvailable = false },
            new CarModel { Id = 6, Brand = "BMW", Model = "M3", Color = "Red", Owner = "Sam Smith", Price = 25000, IsAvailable = true }
            );
    }
}