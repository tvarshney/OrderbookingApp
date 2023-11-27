using System;
using System.Collections.Generic;

namespace FoodOrderingApi.Models;

public partial class Rider
{
    public int? Id { get; set; }

    public Guid RiderId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Available { get; set; }

    public int? PostalCode { get; set; }

    public byte[]? Image { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedDate { get; set; }
}
