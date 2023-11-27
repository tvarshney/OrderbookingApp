namespace FoodOrderingAdminDashboard.Models
{
    public class AdminViewModel
    {
        public Guid AdminId { get; set; }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[] ProfileImage { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }
    }
}
