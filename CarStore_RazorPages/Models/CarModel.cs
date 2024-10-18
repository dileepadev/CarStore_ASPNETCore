using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarStore_RazorPages.Models;

public class CarModel
{
    [Key]
    public int id { get; set; }

    [Display(Name = "Brand")]
    public string? brand { get; set; }
    [Display(Name = "Model")]
    public string? model { get; set; }
    [Display(Name = "Color")]
    public string? color { get; set; }
    [Display(Name = "Owner")]
    public string? owner { get; set; }
    [Display(Name = "Price")]
    public double price { get; set; } = 0;
    [Display(Name = "Is Available")]
    [Required]
    public bool isAvailable { get; set; }
}
