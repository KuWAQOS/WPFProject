using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Order
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public int Orderid { get; set; }

    public int Productcount { get; set; }

    public virtual ICollection<Order> InverseOrderNavigation { get; set; } = new List<Order>();

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
