using System;
using System.Collections.Generic;
using System.Windows.Navigation;
using WpfApp1.Views.OnPagePages;

namespace WpfApp1;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Acess { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Documents { get; set; } = null;

    public event Action<User> EditUserEvent;

    public void EditUser() 
    {
        EditUserEvent?.Invoke(this);
    }

}
