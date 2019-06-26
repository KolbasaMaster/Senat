using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SenatApi
{
    public class SenatContext : DbContext
    {
        public virtual DbSet<ModelIssue> Issues { get; set; }
        public virtual DbSet<ModelMeeting> Meetings { get; set; }
        public virtual DbSet<ModelMember> Members { get; set; }
        public SenatContext()

            : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SenatContext>());
            
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelMember>().HasKey(x => new { x.MemberId, x.IssueId });

            modelBuilder.Entity<ModelIssue>()
                .HasMany<ModelMember>(s => s.ModelMember)
                .WithRequired().HasForeignKey(x => x.IssueId);

            modelBuilder.Entity<ModelIssue>()
                .HasOptional<ModelMeeting>(s => s.Meeting)
                .WithMany(g => g.Issues)
                .HasForeignKey(s => s.MeetingId);
        }

        public DbSet<ModelIssueDto> ModelIssueDtoes { get; set; }
    }
}
