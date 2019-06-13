namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelIssues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CollegialBody = c.Guid(nullable: false),
                        IsInformational = c.Boolean(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Estimate = c.String(),
                        MeetingId = c.Int(nullable: false),
                        Meeting_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModelMeetings", t => t.Meeting_Id)
                .Index(t => t.Meeting_Id);
            
            CreateTable(
                "dbo.ModelMeetings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        _discriminator = c.String(),
                        CollegialBody = c.Guid(nullable: false),
                        Num = c.String(),
                        AgendaDueDate = c.String(),
                        MaterialsDueDate = c.String(),
                        Date = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelIssues", "Meeting_Id", "dbo.ModelMeetings");
            DropIndex("dbo.ModelIssues", new[] { "Meeting_Id" });
            DropTable("dbo.ModelMeetings");
            DropTable("dbo.ModelIssues");
        }
    }
}
