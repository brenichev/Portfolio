
namespace PracticeWebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParticipationList
    {
        public int idPart { get; set; }
        public int StageId { get; set; }
        public int MemberId { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Stage Stage { get; set; }
    }
}
