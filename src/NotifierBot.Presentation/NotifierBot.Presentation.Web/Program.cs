using NotifierBot.Data.Impl;
using NotifierBot.Infrastructure.Maintenance.Enums;
using NotifierBot.Presentation.Web.Components;

const EnvironmentStatus status = EnvironmentStatus.USE_MOCK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .SetupDataAccessLayer(status)
    .AddRazorComponents()
    .AddInteractiveServerComponents();

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

app.Run();