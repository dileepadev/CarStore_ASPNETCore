using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarStore_RazorPages.Models;

public class CarModel
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "brand")]
    public string? brand { get; set; }
    [Display(Name = "model")]
    public string? model { get; set; }
    [Display(Name = "color")]
    public string? color { get; set; }
    [Display(Name = "owner")]
    public string? owner { get; set; }
    [Display(Name = "price")]
    public double price { get; set; } = 0;
    [Display(Name = "isAvailable")]
    [Required]
    public bool isAvailable { get; set; }
}
