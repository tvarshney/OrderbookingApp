﻿using FoodOrderingAdminDashboard.Models;
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
    public class TimingsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly ILogger<DashboardController> _logger;
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public TimingsController(IWebHostEnvironment webHost)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            webHostEnvironment = webHost;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Timings";
            List<TimingViewModel> timingsList = new List<TimingViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Timings/GetTimings").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                timingsList = JsonConvert.DeserializeObject<List<TimingViewModel>>(data);
            }
            return View(timingsList);
        }
        [HttpGet]
        public IActionResult AddTiming(Guid Id)
        {            
            try
            {
                RestaurantViewModel restaurant = new RestaurantViewModel();
                TempData["pageTitle"] = "Timings";                
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
        public IActionResult AddTiming(TimingViewModel model) 
        {
            try
            {
                model.TimingId = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Timings/PostTiming", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Category Created Succussfully";
                    return RedirectToAction("AddTiming");
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
                TimingViewModel timing = new TimingViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Timings/GetTiming/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    timing = JsonConvert.DeserializeObject<TimingViewModel>(data);
                }
                return View(timing);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(TimingViewModel model) 
        {
            try
            {
                Guid id = model.TimingId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Timings/PutTiming/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Timing details updated succussfully";
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
                TimingViewModel timing = new TimingViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Timings/GetTiming/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    timing = JsonConvert.DeserializeObject<TimingViewModel>(data);                   
                }
                return View(timing);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Timings/DeleteTiming/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Timing deleted succussfully";
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
