using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Product
{
    public int Id { get; set; }

    public int Producttypeid { get; set; }

    public string Nameproduct { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int Countofprod { get; set; }

    public string? Producername { get; set; }

    public string? Model { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Producttype Producttype { get; set; } = null!;

    public virtual ICollection<Soldproduct> Soldproducts { get; set; } = new List<Soldproduct>();
}
