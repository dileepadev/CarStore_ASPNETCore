using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarStore_RazorPages.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;

namespace CarStore_RazorPages.Pages
{
    public class EditModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties

        [BindProperty]
        public required CarModel? CarModels { get; set; }


        // Retrieve the data to populate the form for editing
        public async Task OnGet(int id)
        {

            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Retrieve record information to populate the form
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response to populate the form
                using var contentStream = await response.Content.ReadAsStreamAsync();
                CarModels = await JsonSerializer.DeserializeAsync<CarModel>(contentStream);
            }
        }


        // Begin PUT operation code
        public async Task<IActionResult> OnPost()
        {
            // Serialize the information to be edited in the database
            var jsonContent = new StringContent(JsonSerializer.Serialize(CarModels),
                Encoding.UTF8,
                "application/json");

            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Execute the PUT request and store the response. The parameters in PutAsync 
            // appends the item Id to the base address and passes the serialized data to the API
            using HttpResponseMessage response = await httpClient.PutAsync(CarModels!.id.ToString(), jsonContent);

            // Print the response to the console
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            // Return to the home (Index) page and add a temporary success/failure 
            // message to the page.
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Car was edited successfully.";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Operation was not successful";
                return RedirectToPage("Index");
            }

        }
        // End PUT operation code

    }
}

