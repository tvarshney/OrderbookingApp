using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class FoodsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public FoodsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Foods";
            List<FoodViewModel> foodsList = new List<FoodViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Foods/GetFoods").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                foodsList = JsonConvert.DeserializeObject<List<FoodViewModel>>(data);
            }
            return View(foodsList);
        }
        [HttpGet]
        public IActionResult AddFood(Guid Id)
        {            
            try
            {
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
        public IActionResult AddFood(FoodViewModel model) 
        {
            try
            {
                model.FoodId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string uniqueFileName = FoodImage(model);
                model.Image = uniqueFileName;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Foods/PostFood", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Food Created Succussfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        private string FoodImage(FoodViewModel model)
        {
            string uniqueFileName = null;
            if (model.FoodImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FoodImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FoodImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                FoodViewModel food = new FoodViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Foods/GetFood/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    food = JsonConvert.DeserializeObject<FoodViewModel>(data);
                }
                return View(food);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(FoodViewModel model) 
        {
            try
            {
                Guid id = model.FoodId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Foods/PutFood/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Food details updated succussfully";
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
                FoodViewModel food = new FoodViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Foods/GetFood/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    food = JsonConvert.DeserializeObject<FoodViewModel>(data);                   
                }
                return View(food);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Foods/DeleteFood/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Food deleted succussfully";
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
