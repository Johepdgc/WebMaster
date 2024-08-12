using System;
using System.Collections.Generic;

namespace WebMaster.Models;

public partial class SalesProduct
{
    public int Id { get; set; }

    public int SalesId { get; set; }

    public int ProductsId { get; set; }

    public virtual Product Products { get; set; } = null!;

    public virtual Sale Sales { get; set; } = null!;
}
