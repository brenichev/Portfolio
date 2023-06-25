
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManagersList
    {
        public int idForManager { get; set; }
        public int StageId { get; set; }
        public int ManagerId { get; set; }
    
        public virtual Manager Manager { get; set; }
        public virtual Stage Stage { get; set; }
    }
}
