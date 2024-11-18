var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//builder.Services.AddHttpClient("WebAPIDBMClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:5001/api/"); // Base URL da sua API
//});

builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddHttpClient("WebAPIDBMClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
