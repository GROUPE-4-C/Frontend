using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AlumniConnect.Front;
using AlumniConnect.Front.Services;
using Microsoft.AspNetCore.Components.Authorization; // Déjà là, c'est bien !

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- Ajoutez cette ligne cruciale pour l'autorisation ---
builder.Services.AddAuthorizationCore(); 

// Enregistrement de votre fournisseur d'état d'authentification personnalisé
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Enregistrement de vos services d'application
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PromotionService>(); // Assurez-vous que PromotionService est aussi enregistré

// Enregistrement de HttpClient pour les requêtes API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();