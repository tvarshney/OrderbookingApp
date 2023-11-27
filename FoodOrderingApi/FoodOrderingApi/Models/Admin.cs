using System;
using System.Collections.Generic;

namespace FoodOrderingApi.Models;

public partial class Admin
{
    public Guid AdminId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public byte[] ProfileImage { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
}
