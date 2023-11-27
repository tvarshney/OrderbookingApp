namespace FoodOrderingApi.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public Guid FoodId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RestaurantId { get; set;}
    }
}
