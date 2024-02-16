using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotifierBot.Presentation.Services;

namespace NotifierBot.Presentation.Web.Components.Pages;

[IgnoreAntiforgeryToken]
public class Logout(IAuthService authService) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await authService.LogoutAsync();
        return Redirect("/");
    }
}
