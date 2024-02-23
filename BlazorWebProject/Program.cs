using BlazorWebProject.Components;
using BlazorWebProject.Service;
using BlazorWebProject.Model;
using BlazorWebProject.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSingleton<CosmosService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeModel>();
builder.Services.AddScoped<EmployeeController>();
builder.Services.AddScoped<ClientService>();
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
