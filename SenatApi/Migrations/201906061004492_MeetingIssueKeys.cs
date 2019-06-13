namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingIssueKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ModelIssues", new[] { "Meeting_Id" });
            DropColumn("dbo.ModelIssues", "MeetingId");
            RenameColumn(table: "dbo.ModelIssues", name: "Meeting_Id", newName: "MeetingId");
            AlterColumn("dbo.ModelIssues", "MeetingId", c => c.Guid());
            CreateIndex("dbo.ModelIssues", "MeetingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ModelIssues", new[] { "MeetingId" });
            AlterColumn("dbo.ModelIssues", "MeetingId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ModelIssues", name: "MeetingId", newName: "Meeting_Id");
            AddColumn("dbo.ModelIssues", "MeetingId", c => c.Int(nullable: false));
            CreateIndex("dbo.ModelIssues", "Meeting_Id");
        }
    }
}
