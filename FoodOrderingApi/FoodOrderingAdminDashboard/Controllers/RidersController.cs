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
    public class RidersController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public RidersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Riders";
            List<RiderViewModel> ridersList = new List<RiderViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Riders/GetRiders").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ridersList = JsonConvert.DeserializeObject<List<RiderViewModel>>(data);
            }
            return View(ridersList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RiderViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Riders/PostRider", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rider Created Succussfully";
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
                RiderViewModel rider = new RiderViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Riders/GetRider/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    rider = JsonConvert.DeserializeObject<RiderViewModel>(data);
                }
                return View(rider);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(RiderViewModel model) 
        {
            try
            {
                Guid id = model.RiderId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Riders/PutRider/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rider details updated succussfully";
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
                RiderViewModel rider = new RiderViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Riders/GetRider/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    rider = JsonConvert.DeserializeObject<RiderViewModel>(data);                   
                }
                return View(rider);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Riders/DeleteRider/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rider deleted succussfully";
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
