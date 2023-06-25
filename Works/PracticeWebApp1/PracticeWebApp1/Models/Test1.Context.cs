
namespace PracticeWebApp1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EventsTestEntities1 : DbContext
    {
        public EventsTestEntities1()
            : base("name=EventsTestEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adress> Adresses { get; set; }
        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<EventForm> EventForms { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<ManagersList> ManagersLists { get; set; }
        public virtual DbSet<ManagerType> ManagerTypes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<ParticipationList> ParticipationLists { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UsersData> UsersDatas { get; set; }
    }
}
