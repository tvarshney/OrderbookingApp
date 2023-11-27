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
    public class OptionsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public OptionsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Options";
            List<OptionViewModel> optionsList = new List<OptionViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Options/GetOptions").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                optionsList = JsonConvert.DeserializeObject<List<OptionViewModel>>(data);
            }
            return View(optionsList);
        }
        [HttpGet]
        public IActionResult RestaurantOptions(Guid Id)
        {            
            try
            {
                RestaurantViewModel restaurant = new RestaurantViewModel();
                TempData["pageTitle"] = "Options";                
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
        public IActionResult AddOptions(OptionViewModel model) 
        {
            try
            {
                model.OptionId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Options/PostOption", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Option Created Succussfully";
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
                OptionViewModel option = new OptionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Options/GetOption/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    option = JsonConvert.DeserializeObject<OptionViewModel>(data);
                }
                return View(option);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(OptionViewModel model) 
        {
            try
            {
                Guid id = model.OptionId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Options/PutOption/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Option details updated succussfully";
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
                OptionViewModel option = new OptionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Options/GetOption/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    option = JsonConvert.DeserializeObject<OptionViewModel>(data);                   
                }
                return View(option);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Options/DeleteOption/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Option deleted succussfully";
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
