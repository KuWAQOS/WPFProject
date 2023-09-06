using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Producttype
{
    public int Id { get; set; }

    public string Productname { get; set; } = null!;

    public int? Count { get; set; }

    public virtual Product? Product { get; set; }
}
