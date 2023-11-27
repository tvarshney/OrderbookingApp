using System.ComponentModel.DataAnnotations;

namespace FoodOrderingAdminDashboard.Models
{
    public class VendorViewModel
    {
        public Guid VendorId { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
        public virtual ICollection<RestaurantViewModel> Restaurants { get; set; } = new List<RestaurantViewModel>();
    }
}
