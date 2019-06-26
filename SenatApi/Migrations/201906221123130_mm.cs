namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelIssueDtoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsInformational = c.Boolean(nullable: false),
                        Estimate = c.String(),
                        Status = c.Int(nullable: false),
                        MeetingId = c.Guid(),
                        CollegialBody_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identifics", t => t.CollegialBody_Id)
                .ForeignKey("dbo.ModelMeetings", t => t.MeetingId)
                .Index(t => t.MeetingId)
                .Index(t => t.CollegialBody_Id);
            
            CreateTable(
                "dbo.Identifics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ModelMembers", "ModelIssueDto_Id", c => c.Guid());
            CreateIndex("dbo.ModelMembers", "ModelIssueDto_Id");
            AddForeignKey("dbo.ModelMembers", "ModelIssueDto_Id", "dbo.ModelIssueDtoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelMembers", "ModelIssueDto_Id", "dbo.ModelIssueDtoes");
            DropForeignKey("dbo.ModelIssueDtoes", "MeetingId", "dbo.ModelMeetings");
            DropForeignKey("dbo.ModelIssueDtoes", "CollegialBody_Id", "dbo.Identifics");
            DropIndex("dbo.ModelIssueDtoes", new[] { "CollegialBody_Id" });
            DropIndex("dbo.ModelIssueDtoes", new[] { "MeetingId" });
            DropIndex("dbo.ModelMembers", new[] { "ModelIssueDto_Id" });
            DropColumn("dbo.ModelMembers", "ModelIssueDto_Id");
            DropTable("dbo.Identifics");
            DropTable("dbo.ModelIssueDtoes");
        }
    }
}
