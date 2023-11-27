using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingAdminDashboard.Models
{
    public class RestaurantViewModel
    {
        public Guid? VendorId { get; set; }

        public Guid RestaurantId { get; set; }

        public string? RestaurantName { get; set; }

        public string? EmailId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public int? PostalCode { get; set; }

        public string? Image { get; set; }

        public string? Password { get; set; }

        public int? DeliveryTime { get; set; }

        public int? MinimumOrder { get; set; }
        public string? OrderPrifix { get; set; }
        public int? SalesTax { get; set; }

        public string? Available { get; set; }

        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public IFormFile RestaurentImage { get; set; } 
        public string? VendorEmail { get; set; }
        public virtual ICollection<FoodViewModel> Foods { get; set; } = new List<FoodViewModel>();
        public virtual ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public virtual ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public virtual ICollection<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();
        public virtual ICollection<AddonViewModel> Addons { get; set; } = new List<AddonViewModel>();
        public virtual ICollection<TimingViewModel> Timings { get; set; } = new List<TimingViewModel>();

    }
}
