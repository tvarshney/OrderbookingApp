using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using static NuGet.Packaging.PackagingConstants;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public OrdersController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Orders";
            List<OrderViewModel> ordersList = new List<OrderViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetOrders").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ordersList = JsonConvert.DeserializeObject<List<OrderViewModel>>(data);
            }
            return View(ordersList);
        }
        [HttpGet]
        public IActionResult RestaurantOrders(Guid Id)
        {            
            try
            {
                RestaurantViewModel restaurant = new RestaurantViewModel();
                TempData["pageTitle"] = "Orders";                
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
        public IActionResult AddCategory(CategoryViewModel model) 
        {
            try
            {
                model.CategoryId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Categories/PostCategory", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Category Created Succussfully";
                    return RedirectToAction("AddCategory");
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
                CategoryViewModel category = new CategoryViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Categories/GetCategory/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<CategoryViewModel>(data);
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model) 
        {
            try
            {
                Guid id = model.CategoryId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Categories/PutCategory/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Category details updated succussfully";
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
                CategoryViewModel category = new CategoryViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Categories/GetCategory/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<CategoryViewModel>(data);                   
                }
                return View(category);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Categories/DeleteCategory/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Category deleted succussfully";
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
