
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Stages = new HashSet<Stage>();
        }
    
        public int idEvents { get; set; }
        public string EventName { get; set; }
        public int Typeid { get; set; }
        public int Ageid { get; set; }
        public int Formid { get; set; }
        public string EventLink { get; set; }
        public string EventDesc { get; set; }
    
        public virtual Age Age { get; set; }
        public virtual EventForm EventForm { get; set; }
        public virtual EventType EventType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
