using BlazorWebProject.Components;
using BlazorWebProject.Service;
using BlazorWebProject.Model;
using BlazorWebProject.Controller;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddScoped<EmployeeModel>();
builder.Services.AddScoped<DepartmentModel>();
builder.Services.AddSingleton<CosmosService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthController>();
builder.Services.AddScoped<EmployeeController>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
HttpClient BaseAddressConfigure(IServiceProvider services)
{
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://localhost:7050/");
    return httpClient;
}
builder.Services.AddScoped<HttpClient>(BaseAddressConfigure);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
