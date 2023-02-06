using Fly.Core.Parameters;
using Fly.WebUI.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Fly.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return View();
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    public IActionResult Search(FlightParameter parameters)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Secret()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var idToken = await HttpContext.GetTokenAsync("id_token");
        var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

        var claims = User.Claims.ToList();
        var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);

        var result = await GetSecret(accessToken);

        await RefreshAccessToken();

        return View();
    }

    public IActionResult Logout()
    {
        return SignOut("Cookie", "oidc");
    }

    public async Task<string> GetSecret(string accessToken)
    {
        var apiClient = _httpClientFactory.CreateClient();

        apiClient.SetBearerToken(accessToken);

        var response = await apiClient.GetAsync("https://localhost:5000/api/aircrafts");
        var content = await response.Content.ReadAsStringAsync();

        return content;
    }

    private async Task RefreshAccessToken()
    {
        var serverClient = _httpClientFactory.CreateClient();
        var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:5004/");

        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var idToken = await HttpContext.GetTokenAsync("id_token");
        var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
        var refreshTokenClient = _httpClientFactory.CreateClient();

        var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(
            new RefreshTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                RefreshToken = refreshToken,
                ClientId = "web1",
                ClientSecret = "secret"
            });

        var authInfo = await HttpContext.AuthenticateAsync("Cookie");

        authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
        authInfo.Properties.UpdateTokenValue("id_token", tokenResponse.IdentityToken);
        authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);

        await HttpContext.SignInAsync("Cookie", authInfo.Principal, authInfo.Properties);
    }
}
