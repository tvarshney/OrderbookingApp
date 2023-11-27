using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodOrderingApi.Models;

public partial class Food
{
    public int? Id { get; set; }

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

    public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
    [JsonIgnore]
    public virtual Restaurant? Restaurant { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
