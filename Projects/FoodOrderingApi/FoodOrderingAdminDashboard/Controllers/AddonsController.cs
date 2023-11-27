using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using static NuGet.Packaging.PackagingConstants;
using Microsoft.Extensions.Options;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class AddonsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public AddonsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Addons";
            List<AddonViewModel> addonsList = new List<AddonViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Addons/GetAddons").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                addonsList = JsonConvert.DeserializeObject<List<AddonViewModel>>(data);
            }
            return View(addonsList);
        }
        [HttpGet]
        public IActionResult RestaurantAddons(Guid Id)
        {            
            try
            {
                RestaurantViewModel restaurant = new RestaurantViewModel();
                TempData["pageTitle"] = "Addons";                
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
        public IActionResult AddAddons(AddonViewModel model) 
        {
            try
            {
                model.AddonId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Addons/PostAddon", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Addon Created Succussfully";
                    return RedirectToAction("RestaurantOptions");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                AddonViewModel addon = new AddonViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Addons/GetAddon/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    addon = JsonConvert.DeserializeObject<AddonViewModel>(data);
                }
                return View(addon);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(AddonViewModel model) 
        {
            try
            {
                Guid id = model.AddonId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Addons/PutAddon/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Addon details updated succussfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                AddonViewModel addon = new AddonViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Addons/GetAddon/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    addon = JsonConvert.DeserializeObject<AddonViewModel>(data);                   
                }
                return View(addon);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Addons/DeleteAddon/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Addon deleted succussfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
