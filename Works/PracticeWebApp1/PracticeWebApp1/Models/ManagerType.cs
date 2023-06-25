
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManagerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManagerType()
        {
            this.Managers = new HashSet<Manager>();
        }
    
        public int idManagerType { get; set; }
        public string ManagerType1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manager> Managers { get; set; }
    }
}
