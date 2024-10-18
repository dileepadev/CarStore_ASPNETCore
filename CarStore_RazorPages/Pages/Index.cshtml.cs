using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarStore_RazorPages.Models;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components;

namespace CarStore_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Add the data model and bind the form data to the page model properties
        // Enumerable since an array is expected as a response
        [BindProperty]
        public IEnumerable<CarModel> CarModels { get; set; }

        // Begin GET operation code
        // OnGet() is async since HTTP requests should be performed async
        public async Task OnGet()
        {
            // Create the HTTP client using the CarStore_MinimalAPI named factory
            var httpClient = _httpClientFactory.CreateClient("CarStore_MinimalAPI");

            // Perform the GET request and store the response. The empty parameter
            // in GetAsync doesn't modify the base address set in the client factory 
            using HttpResponseMessage response = await httpClient.GetAsync("");

            // Read the response content as a string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Log the response content to the console
            Console.WriteLine(responseContent);

            // If the request is successful deserialize the results into the data model
            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                CarModels = await JsonSerializer.DeserializeAsync<IEnumerable<CarModel>>(contentStream);
            }
        }
        // End GET operation code
    }
}