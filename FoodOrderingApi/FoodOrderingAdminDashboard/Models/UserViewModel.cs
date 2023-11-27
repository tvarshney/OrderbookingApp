using System.ComponentModel.DataAnnotations;

namespace FoodOrderingAdminDashboard.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? HomeAddress { get; set; }

        public string? OfficeAddress { get; set; }

        public string? OtherAddress { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public int? PostalCode { get; set; }

        public byte[]? Image { get; set; }
        [Required]
        public string Password { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
    }
}
