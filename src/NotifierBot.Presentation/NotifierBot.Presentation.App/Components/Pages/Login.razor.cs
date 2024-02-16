using System.Net;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Presentation.Models;
using NotifierBot.Presentation.Services;

namespace NotifierBot.Presentation.App.Components.Pages;

/// <summary>
///     Страница входа
/// </summary>
public partial class Login
{
    private AuthModel _model = new(string.Empty, string.Empty);
    private MudForm _form;

    /// <summary>
    ///     Контекст HTTP 
    /// </summary>
    [Inject]
    public IHttpContextAccessor HttpContext { get; set; } = default!;

    /// <summary>
    ///     Менеджкер навигации
    /// </summary>
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    /// <summary>
    ///     Сервис авторизации
    /// </summary>
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (HttpContext.HttpContext?.User.Identity?.IsAuthenticated == true)
        {
            NavigationManager.NavigateTo("/");
        }
    }

    /// <summary>
    ///     Обработчик нажатия кнопки Login
    /// </summary>
    private async Task OnValidSubmit()
    {
        try
        {
            await AuthService.LoginAsync(_model.Login, _model.Password);
        }
        catch (ErrorException ex) when (ex.Code == HttpStatusCode.Forbidden)
        {
            return;
        }

        NavigationManager.NavigateTo("/");
    }
}