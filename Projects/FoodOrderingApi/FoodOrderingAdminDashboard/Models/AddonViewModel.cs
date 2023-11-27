namespace FoodOrderingAdminDashboard.Models
{
    public class AddonViewModel
    {
        public int? Id { get; set; }

        public Guid? VendorId { get; set; }

        public Guid? RestaurantId { get; set; }

        public Guid? FoodId { get; set; }

        public Guid AddonId { get; set; }

        public string? Title { get; set; }
        public string Description { get; set; }
        public int? MinimumQuantity { get; set; }

        public int? MaximumQuantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        //public virtual Food? Food { get; set; }
        public string Option { get; set; }
        public virtual ICollection<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();
    }
}
