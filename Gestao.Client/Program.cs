using Gestao.Client;
using Gestao.Client.Services;
using Gestao.Domain.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<HttpClient>(options =>
{
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("http://localhost:5186");//builder.HostEnvironment.BaseAddress);
    //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    return httpClient;
});

builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionService>();

await builder.Build().RunAsync();
