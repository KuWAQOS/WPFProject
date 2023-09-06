using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Soldproduct
{
    public int Id { get; set; }

    public int Clientid { get; set; }

    public int Productid { get; set; }

    public DateOnly Dateofsold { get; set; }

    public int Orderid { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
