var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add IHttpClientFactory to the container and set the name of the factory
// to "CarStore_MinimalAPI". The base address for API requests is also set.
builder.Services.AddHttpClient("CarStore_MinimalAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:5062/carlist/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
