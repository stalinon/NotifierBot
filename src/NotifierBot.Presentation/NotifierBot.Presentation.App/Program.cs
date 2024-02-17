using Microsoft.AspNetCore.Authorization;
using MudBlazor.Services;
using NotifierBot.Business.Impl;
using NotifierBot.Infrastructure.Maintenance;
using NotifierBot.Presentation.App.Components;
using NotifierBot.Presentation.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.SetupBusinessLayer().AddPresentationLayer();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMudServices();

builder.Services.AddAuthentication(
        o =>
        {
            o.DefaultScheme = Config.AUTH_SCHEME;
            o.DefaultChallengeScheme = Config.AUTH_SCHEME;
        }
    )
    .AddCookie(
        Config.AUTH_SCHEME,
        options => { }
    );

builder.Services.AddAuthorization(
    options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(Config.AUTH_SCHEME)
            .Build();
    }
);

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