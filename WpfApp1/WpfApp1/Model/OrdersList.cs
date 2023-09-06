using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class OrdersList
{
    public int Id { get; set; }

    public int Clientid { get; set; }

    public decimal Sum { get; set; }    

    public DateOnly Dateofcreate { get; set; }

    public virtual Client Client { get; set; } = null!;
}
