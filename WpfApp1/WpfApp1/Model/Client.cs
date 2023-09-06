using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public decimal? Ransom { get; set; }

    public int? Discount { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<OrdersList> Order1s { get; set; } = new List<OrdersList>();

    public virtual ICollection<Soldproduct> Soldproducts { get; set; } = new List<Soldproduct>();
}
