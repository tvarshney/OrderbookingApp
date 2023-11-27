using System.ComponentModel.DataAnnotations;

namespace FoodOrderingAdminDashboard.Models
{
    public class RestaurantSectionViewModel
    {
        public virtual ICollection<RestaurantSectionModel> RestaurantSection { get; set; } = new List<RestaurantSectionModel>();
        public virtual ICollection<RestaurantViewModel> RestaurantsData { get; set; } = new List<RestaurantViewModel>();
           
    }
    public class RestaurantSectionModel
    {
        public Guid RestaurantSectionId { get; set; }

        public string? Name { get; set; }
        public string? Status { get; set; }

        public string? Restaurants { get; set; }

        public DateTime? CreatedDate { get; set; }        
    }
}
