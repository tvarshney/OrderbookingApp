using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodOrderingApi.Models;

public partial class RestaurantSection
{
    public Guid RestaurantSectionId { get; set; }

    public string? Name { get; set; }
    public string? Status { get; set; }

    public string? Restaurants { get; set; }

    public DateTime? CreatedDate { get; set; }
}
