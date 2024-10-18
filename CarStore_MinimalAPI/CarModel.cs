namespace CarStore_MinimalAPI;

public class CarModel
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string? Owner { get; set; }
    public double Price { get; set; } = 0;
    public required bool IsAvailable { get; set; }
}