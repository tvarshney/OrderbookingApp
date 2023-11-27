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
    public class RestaurantSectionsController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public RestaurantSectionsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Restaurant Sections";
            RestaurantSectionViewModel restaurantSectionsList = new RestaurantSectionViewModel();
            HttpResponseMessage restaurantSectionsresponse = _client.GetAsync(_client.BaseAddress + "/RestaurantSections/GetRestaurantSections").Result;
            HttpResponseMessage restaurantsresponse = _client.GetAsync(_client.BaseAddress + "/Restaurants/GetRestaurants").Result;
            if (restaurantSectionsresponse.IsSuccessStatusCode)
            {
                string restaurantSectionsdata = restaurantSectionsresponse.Content.ReadAsStringAsync().Result;
                string restaurantdata = restaurantsresponse.Content.ReadAsStringAsync().Result;
                restaurantSectionsList.RestaurantSection = JsonConvert.DeserializeObject<List<RestaurantSectionModel>>(restaurantSectionsdata);
                restaurantSectionsList.RestaurantsData = JsonConvert.DeserializeObject<List<RestaurantViewModel>>(restaurantdata);
            }
            return View(restaurantSectionsList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RestaurantSectionModel model) 
        {
            try
            {
                model.RestaurantSectionId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/RestaurantSections/PostRestaurantSection", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant Section Created Succussfully";
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
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                RestaurantSectionViewModel restaurantSection = new RestaurantSectionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/RestaurantSections/GetRestaurantSection/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    restaurantSection = JsonConvert.DeserializeObject<RestaurantSectionViewModel>(data);
                }
                return View(restaurantSection);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(RestaurantSectionModel model) 
        {
            try
            {
                Guid id = model.RestaurantSectionId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/RestaurantSections/PutRestaurantSection/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant Section details updated succussfully";
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
                RestaurantSectionModel restaurantSection = new RestaurantSectionModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/RestaurantSections/GetRestaurantSection/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    restaurantSection = JsonConvert.DeserializeObject<RestaurantSectionModel>(data);                   
                }
                return View(restaurantSection);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/RestaurantSections/DeleteRestaurantSection/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Restaurant Section deleted succussfully";
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
