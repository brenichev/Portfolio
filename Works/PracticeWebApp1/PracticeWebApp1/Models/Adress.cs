
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adress()
        {
            this.Stages = new HashSet<Stage>();
        }
    
        public int idAdress { get; set; }
        public string Adress1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
