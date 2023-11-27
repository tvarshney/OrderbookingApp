using FoodOrderingAdminDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace FoodOrderingAdminDashboard.Controllers
{
    [Authorize]
    public class VendorsController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5019/api");
        private readonly HttpClient _client;
        public VendorsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            TempData["pageTitle"] = "Vendors";
            List<VendorViewModel> vendorsList = new List<VendorViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendors").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                vendorsList = JsonConvert.DeserializeObject<List<VendorViewModel>>(data);
            }
            return View(vendorsList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(VendorViewModel model) 
        {
            try
            {
                if (model.EmailId != null && model.Password != null)
                {
                    model.VendorId = Guid.NewGuid();
                    model.CreatedDate = DateTime.Now;
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Vendors/PostVendor", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Vendor Created Succussfully";
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
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                VendorViewModel vendor = new VendorViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    vendor = JsonConvert.DeserializeObject<VendorViewModel>(data);
                }
                return View(vendor);
            }
            catch (Exception ex)
            {
                TempData["errorMesssage"] = ex.Message;
                return View();  
            }
            
        }
        [HttpPost]
        public IActionResult Edit(VendorViewModel model) 
        {
            try
            {
                Guid id = model.VendorId;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Vendord/PutVendor/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Vendor details updated succussfully";
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
                VendorViewModel vendor = new VendorViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Vendors/GetVendor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    vendor = JsonConvert.DeserializeObject<VendorViewModel>(data);                   
                }
                return View(vendor);
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
                    TempData["successMessage"] = "Vendor deleted succussfully";
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
