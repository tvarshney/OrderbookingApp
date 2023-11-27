using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class RestaurantsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public RestaurantsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Restaurants";
            List<RestaurantViewModel> restaurantsList = new List<RestaurantViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Restaurants/GetRestaurants").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                restaurantsList = JsonConvert.DeserializeObject<List<RestaurantViewModel>>(data);
            }
            return View(restaurantsList);
        }
        [HttpGet]
        public IActionResult VendorRestaurants(Guid Id)
        {
            TempData["pageTitle"] = "Restaurants";
            VendorViewModel vendor = new VendorViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendor/"+Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                vendor = JsonConvert.DeserializeObject<VendorViewModel>(data);
            }
            return View(vendor);            
        }
        [HttpPost]
        public IActionResult CreateRestaurant([FromForm]RestaurantViewModel model) 
        {
            try
            {
                if (model.EmailId != null && model.Password != null)
                {
                    Random random = new Random();
                    String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    int size = 5;
                    String ran = "";
                    for (int i = 0; i < size; i++)
                    {
                        // Selecting a index randomly
                        int x = random.Next(36);

                        // Appending the character at the 
                        // index to the random string.
                        model.OrderPrifix = ran + str[x];
                    }
                    //model.VendorId = 
                    model.RestaurantId = Guid.NewGuid();
                    model.CreatedDate = DateTime.Now;
                    string uniqueFileName = UploadImage(model);
                    model.Image = uniqueFileName;
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Restaurants/PostRestaurant", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Restaurant Created Succussfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        private string UploadImage(RestaurantViewModel model)
        {
            string uniqueFileName = null;
            if(model.RestaurentImage != null)
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
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                RestaurantViewModel restaurant = new RestaurantViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendor/" + id).Result;
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
        public IActionResult Edit(RestaurantViewModel model) 
        {
            try
            {
                Guid id = model.RestaurantId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Vendord/PutVendor/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant details updated succussfully";
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
                RestaurantViewModel restaurant = new RestaurantViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendor/" + id).Result;
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
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Vendors/DeleteVendor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant deleted succussfully";
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
