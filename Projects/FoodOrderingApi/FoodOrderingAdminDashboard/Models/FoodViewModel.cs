using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingAdminDashboard.Models
{
    public class FoodViewModel
    {
        public Guid? VendorId { get; set; }

        public Guid? RestaurantId { get; set; }

        public Guid FoodId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Image { get; set; }

        public string? VariationsTitle { get; set; }

        public int? Price { get; set; }

        public int? Discount { get; set; }

        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public IFormFile FoodImage { get; set; }

        //public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
        public virtual ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    }
}
