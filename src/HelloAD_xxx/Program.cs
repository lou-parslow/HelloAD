using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HelloAD;
using HelloAD.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMsalAuthentication(options =>
{
	builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
	options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Files.Read");
	options.ProviderOptions.LoginMode = "redirect";
	options.ProviderOptions.Authentication.Authority = "https://login.microsoftonline.com/common/";
	options.ProviderOptions.Authentication.ValidateAuthority = true;
	options.ProviderOptions.Authentication.ClientId = "ab33fa7e-f310-4ae7-bd60-40ecf4a21bd3";
});
builder.Services.AddScoped<OneDriveService>();



await builder.Build().RunAsync();
