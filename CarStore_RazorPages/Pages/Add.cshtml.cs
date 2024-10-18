using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarStore_RazorPages.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;


namespace CarStore_RazorPages.Pages
{
    public class AddModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties

        [BindProperty]
        public required CarModel CarModels { get; set; }

        // Begin POST operation code
        public async Task<IActionResult> OnPost()
        {
            // Serialize the information to be added to the database
            var jsonContent = new StringContent(JsonSerializer.Serialize(CarModels),
                Encoding.UTF8,
                "application/json");

            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Execute the POST request and store the response. The parameters in PostAsync 
            // direct the POST to use the base address and passes the serialized data to the API
            using HttpResponseMessage response = await httpClient.PostAsync("", jsonContent);

            // Return to the home (Index) page and add a temporary success/failure 
            // message to the page.
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Car was added successfully.";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("Index");
            }
        }
        // End POST operation code
    }
}

