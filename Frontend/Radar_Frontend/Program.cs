using MudBlazor.Services;
using Radar_Frontend;
using Radar_Frontend.Components;
using Radar_Frontend.Components.Utilities.Services;
using radar_frontend.Interfaces;
using radar_frontend.Models;
using radar_frontend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Register MudBlazor services
builder.Services.AddMudServices();

// Register MqttService as a singleton
builder.Services.AddSingleton<MqttService>();

builder.Services.AddScoped<ISensorRepository, SensorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
