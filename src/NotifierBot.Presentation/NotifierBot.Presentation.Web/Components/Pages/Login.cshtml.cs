using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Presentation.Services;

namespace NotifierBot.Presentation.Web.Components.Pages;

public class Login(IAuthService authService) : PageModel
{
    [BindProperty]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    [BindProperty]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public string? ErrorMessage { get; private set; }

    public IActionResult OnGet()
    {
        if (HttpContext.User.Identity?.IsAuthenticated == true)
        {
            return Redirect("/");
        }

        return Page();
    }

    /// <summary>
    ///     Обработчик нажатия кнопки Login
    /// </summary>
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await authService.LoginAsync(Email, Password);
        }
        catch (ErrorException ex) when (ex.Code == HttpStatusCode.Forbidden)
        {
            ErrorMessage = ex.Message;
            return Page();
        }

        return LocalRedirect(Url.Content("~/"));
    }
}