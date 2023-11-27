namespace FoodOrderingAdminDashboard.Models
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
