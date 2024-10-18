using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarStore_RazorPages.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;

namespace CarStore_RazorPages.Pages
{
    public class DeleteModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties

        [BindProperty]
        public required CarModel? CarModels { get; set; }


        // Retrieve the data to populate the form for deletion
        public async Task OnGet(int id)
        {

            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Retrieve record information
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response to populate the form
                using var contentStream = await response.Content.ReadAsStreamAsync();
                CarModels = await JsonSerializer.DeserializeAsync<CarModel>(contentStream);
            }
        }


        // Begin DELETE operation code
        public async Task<IActionResult> OnPost()
        {
            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Appends the data Id for deletion to the base address and performs the operation
            using HttpResponseMessage response = await httpClient.DeleteAsync(CarModels!.id.ToString());

            // Return to the home (Index) page and add a temporary success/failure 
            // message to the page.
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Car was deleted successfully.";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("Index");
            }

        }
        // End DELETE operation code

    }
}

