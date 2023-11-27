using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public DashboardController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Restaurant Dashboard";
            return View();
        }
        [HttpGet]
        public IActionResult RestaurantDashboard(Guid Id)
        {
            TempData["pageTitle"] = "Restaurant Dashboard";
            RestaurantViewModel restaurant = new RestaurantViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Restaurants/GetRestaurant/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                restaurant = JsonConvert.DeserializeObject<RestaurantViewModel>(data);
            }
            return View(restaurant);
            //return View();
        }
        [HttpGet]
        public IActionResult RestaurantProfile(Guid Id)
        {
            try
            {
                TempData["pageTitle"] = "Restaurant Profile";
                RestaurantViewModel restaurant = new RestaurantViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Restaurants/GetRestaurant/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    restaurant = JsonConvert.DeserializeObject<RestaurantViewModel>(data);
                }
                return View(restaurant);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult RestaurantProfileUpdate(RestaurantViewModel model)
        {
            try
            {
                Guid id = model.RestaurantId;
                string uniqueFileName = UpdateImage(model);
                model.Image = uniqueFileName;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");                
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Restaurants/PutRestaurant/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant Profile details updated succussfully";
                    return RedirectToAction("RestaurantDashboard");
                }                
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();
            }
            return View();
        }
        private string UpdateImage(RestaurantViewModel model)
        {
            string uniqueFileName = null;
            if (model.RestaurentImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.RestaurentImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.RestaurentImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","AdminLogin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}