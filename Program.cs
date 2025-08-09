using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AlumniConnect.Front;
using AlumniConnect.Front.Services;
using AlumniConnect.Front.Services.Chat;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration de l'URL de base de l'API
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5175";

// Configuration du HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

// Enregistrement des services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PromotionService>();
builder.Services.AddScoped<ChatService>();

await builder.Build().RunAsync();
