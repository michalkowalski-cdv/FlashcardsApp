using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;
using BlazorApp1.Services;
using Supabase; 
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<Supabase.Client>(provider => 
    new Supabase.Client(Config.SupabaseUrl, Config.SupabaseKey));

builder.Services.AddScoped<IFlashcardService, SupabaseFlashcardService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserService>();

await builder.Build().RunAsync();