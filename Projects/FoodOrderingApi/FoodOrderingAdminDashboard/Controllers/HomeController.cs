using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
                
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Home";
            List<UserViewModel> usersList = new List<UserViewModel>();
            List<VendorViewModel> vendorsList = new List<VendorViewModel>();
            List<RestaurantViewModel> restaurantList = new List<RestaurantViewModel>();
            List<RiderViewModel> riderList = new List<RiderViewModel>();
            Graph graph = new Graph();
            HttpResponseMessage userResponse = _client.GetAsync(_client.BaseAddress + "/Users/GetUsers").Result;
            HttpResponseMessage vendorResponse = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendors").Result;
            HttpResponseMessage restaurantResponse = _client.GetAsync(_client.BaseAddress + "/Restaurants/GetRestaurants").Result;
            HttpResponseMessage riderResponse = _client.GetAsync(_client.BaseAddress + "/Riders/GetRiders").Result;
            if (userResponse.IsSuccessStatusCode && vendorResponse.IsSuccessStatusCode && restaurantResponse.IsSuccessStatusCode && riderResponse.IsSuccessStatusCode)
            {
                string userData = userResponse.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(userData);
                string vendorData = vendorResponse.Content.ReadAsStringAsync().Result;
                vendorsList = JsonConvert.DeserializeObject<List<VendorViewModel>>(vendorData);
                string restaurantData = restaurantResponse.Content.ReadAsStringAsync().Result;
                restaurantList = JsonConvert.DeserializeObject<List<RestaurantViewModel>>(restaurantData);
                string riderData = riderResponse.Content.ReadAsStringAsync().Result;
                riderList = JsonConvert.DeserializeObject<List<RiderViewModel>>(riderData);
                graph.Users = usersList.Count();
                graph.Vendors = vendorsList.Count();
                graph.Restaurants = restaurantList.Count();
                graph.Riders = riderList.Count();
            }
            return View(graph);
            //return View();
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