namespace FoodOrderingAdminDashboard.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string Items { get; set; }
        public string PaymantMode { get; set; }
        public string Status { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Address { get; set; }
        public Guid? RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
