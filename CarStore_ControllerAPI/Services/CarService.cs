using CarStore_ControllerAPI.Models;

namespace CarStore_ControllerAPI.Services;

public static class CarService
{
    static List<CarModel> Cars { get; }
    static CarService()
    {
        Cars =
        [
            new CarModel { Id = 1, Brand = "Toyota", Model = "Corolla", Color = "White", Owner = "John Doe", Price = 15000, IsAvailable = true },
            new CarModel { Id = 2, Brand = "Honda", Model = "Civic", Color = "Black", Owner = "Jane Doe", Price = 20000, IsAvailable = false },
            new CarModel { Id = 3, Brand = "Ford", Model = "Fusion", Color = "Red", Owner = "Sam Smith", Price = 25000, IsAvailable = true },
            new CarModel { Id = 4, Brand = "Chevrolet", Model = "Cruze", Color = "Blue", Owner = "John Doe", Price = 15000, IsAvailable = true },
            new CarModel { Id = 5, Brand = "Nissan", Model = "Sentra", Color = "Black", Owner = "Jane Doe", Price = 20000, IsAvailable = false },
            new CarModel { Id = 6, Brand = "BMW", Model = "M3", Color = "Red", Owner = "Sam Smith", Price = 25000, IsAvailable = true },
        ];
    }

    public static List<CarModel> GetAll() => Cars;

    public static CarModel? Get(int id) => Cars.FirstOrDefault(p => p.Id == id);

    public static void Add(CarModel Car)
    {
        Car.Id = Cars.Count + 1;
        Cars.Add(Car);
    }

    public static void Delete(int id)
    {
        var Car = Get(id);
        if (Car is null)
            return;

        Cars.Remove(Car);
    }

    public static void Update(CarModel Car)
    {
        var index = Cars.FindIndex(p => p.Id == Car.Id);
        if (index == -1)
            return;

        Cars[index] = Car;
    }
}
