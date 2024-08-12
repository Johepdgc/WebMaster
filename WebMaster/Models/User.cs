using System;
using System.Collections.Generic;

namespace WebMaster.Models;

public partial class User
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;
}
