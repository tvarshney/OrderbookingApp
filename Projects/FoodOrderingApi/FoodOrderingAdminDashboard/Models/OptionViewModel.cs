namespace FoodOrderingAdminDashboard.Models
{
    public class OptionViewModel
    {
        public int? Id { get; set; }

        public Guid? VendorId { get; set; }

        public Guid? RestaurantId { get; set; }

        public Guid? FoodId { get; set; }

        public Guid? AddonId { get; set; }

        public string Description { get; set; }
        public Guid OptionId { get; set; }

        public string? Title { get; set; }

        public int? Price { get; set; }

        public DateTime? CreatedDate { get; set; }

        //public virtual Addon? Addon { get; set; }
    }
}
