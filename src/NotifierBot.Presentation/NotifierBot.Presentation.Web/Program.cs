using NotifierBot.Business.Impl;
using NotifierBot.Data.Impl;
using NotifierBot.Infrastructure.Maintenance.Enums;
using NotifierBot.Presentation.Impl;
using NotifierBot.Presentation.Web;
using NotifierBot.Presentation.Web.Components;

const EnvironmentStatus status = EnvironmentStatus.USE_MOCK;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services
    .SetupBusinessLayer()
    .SetupDataAccessLayer(status);

builder.Services.AddRazorComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAuthService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();