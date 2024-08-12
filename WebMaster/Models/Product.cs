using System;
using System.Collections.Generic;

namespace WebMaster.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<SalesProduct> SalesProducts { get; set; } = new List<SalesProduct>();
}
