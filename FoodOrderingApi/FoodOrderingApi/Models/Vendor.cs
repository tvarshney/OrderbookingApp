using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodOrderingApi.Models;

public partial class Vendor
{
    public Guid VendorId { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedDate { get; set; }
    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
