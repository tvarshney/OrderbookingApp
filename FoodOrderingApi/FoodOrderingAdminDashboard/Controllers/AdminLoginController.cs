using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodOrderingAdminDashboard.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly ILogger<AdminLoginController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public AdminLoginController(ILogger<AdminLoginController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
                
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            TempData["pageTitle"] = "Home";
            if( claimsUser.Identity.IsAuthenticated ) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            List<AdminViewModel> adminList = new List<AdminViewModel>();
            
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admins/GetAdmins").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var List = JsonConvert.DeserializeObject<List<AdminViewModel>>(data);
                adminList = List.Where(a => a.Username == model.Email && a.Password == model.Password).ToList();
            }
            //if (admin.Username == model.Email &&  admin.Password == model.Password)
            if(adminList.Count() > 0)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim("OtherProperties","Example Role")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.KeepLoggedIn
                };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}