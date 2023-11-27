namespace FoodOrderingAdminDashboard.Models
{
    public class TimingViewModel
    {
        public Guid TimingId { get; set; }
        public Guid RestaurantId { get; set; }
        public string Day { get; set; }
        public string OpenTiming { get; set; }
        public string CloseTiming { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
