using System;
using System.Collections.Generic;

namespace WebMaster.Models;

public partial class Sale
{
    public int Id { get; set; }

    public string Client { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? PaidDate { get; set; }

    public bool IsPaid { get; set; }

    public virtual ICollection<SalesProduct> SalesProducts { get; set; } = new List<SalesProduct>();
}
