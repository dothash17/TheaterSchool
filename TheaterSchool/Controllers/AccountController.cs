using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TheaterSchoolDB.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string user)
    {
        await AuthAsync(user);
        return RedirectToAction(nameof(User));
    }

    public ActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public ActionResult User()
    {
        return View();
    }

    #region private methods

    private async Task AuthAsync(string user)
    {
        List<Claim> claims = new();

        switch (user)
        {
            case "admin":
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, "qwer"));
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
                break;
            default:
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, "qwer"));
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"));
                break;
        }

        // создаем объект ClaimsIdentity
        ClaimsIdentity identity = new(claims, "Cookies", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));
    }
    #endregion
}