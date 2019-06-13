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
        public DbSet<ModelIssue> Issues { get; set; }
        public DbSet<ModelMeeting> Meetings { get; set; }
        public DbSet<ModelMember> Members { get; set; }
        public SenatContext()

            : base("DefaultConnection")
        {
           
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

    }
}
