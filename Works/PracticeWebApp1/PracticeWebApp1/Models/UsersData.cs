
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersData
    {
        public int idLogin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Mod { get; set; }
    }
}
