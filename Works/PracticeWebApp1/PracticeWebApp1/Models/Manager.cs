
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manager()
        {
            this.ManagersLists = new HashSet<ManagersList>();
        }
    
        public int idManager { get; set; }
        public string ManagerSurname { get; set; }
        public string ManagerName { get; set; }
        public string ManagerOtch { get; set; }
        public int ManagerTypeid { get; set; }
        public string ManagerLink { get; set; }
        public string ManagerDesc { get; set; }
    
        public virtual ManagerType ManagerType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagersList> ManagersLists { get; set; }
    }
}
