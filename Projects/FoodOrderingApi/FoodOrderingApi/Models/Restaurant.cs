using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodOrderingApi.Models;

public partial class Restaurant
{
    public int? Id { get; set; }

    public Guid? VendorId { get; set; }

    public Guid RestaurantId { get; set; }

    public string? RestaurantName { get; set; }

    public string? EmailId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public int? PostalCode { get; set; }

    public string? Image { get; set; }

    public string? Password { get; set; }

    public int? DeliveryTime { get; set; }

    public int? MinimumOrder { get; set; }

    public string? OrderPrifix { get; set; }

    public int? SalesTax { get; set; }

    public string? Available { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? FoodId { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); 
    public virtual ICollection<Option> Options { get; set; } = new List<Option>();
    public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
    public virtual ICollection<Timing> Timings { get; set; } = new List<Timing>();
    [JsonIgnore]
    public virtual Vendor? Vendor { get; set; }
}
