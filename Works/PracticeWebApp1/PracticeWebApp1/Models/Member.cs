
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            this.ParticipationLists = new HashSet<ParticipationList>();
        }
    
        public int idMember { get; set; }
        public string MemberSurname { get; set; }
        public string MemberName { get; set; }
        public string MemberOtch { get; set; }
        public int MemberTypeId { get; set; }
        public string MemberDesc { get; set; }
        public string MemberLink { get; set; }
    
        public virtual MemberType MemberType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParticipationList> ParticipationLists { get; set; }
    }
}
