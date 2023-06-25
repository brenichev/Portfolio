
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stage()
        {
            this.ManagersLists = new HashSet<ManagersList>();
            this.ParticipationLists = new HashSet<ParticipationList>();
        }
    
        public int idStage { get; set; }
        public int StageNumber { get; set; }
        public int EventId { get; set; }
        public string StageName { get; set; }
        public int AdressId { get; set; }
        public string House { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateFinish { get; set; }
        public Nullable<double> StageCost { get; set; }
        public string StageDesc { get; set; }
    
        public virtual Adress Adress { get; set; }
        public virtual Event Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagersList> ManagersLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParticipationList> ParticipationLists { get; set; }
    }
}
