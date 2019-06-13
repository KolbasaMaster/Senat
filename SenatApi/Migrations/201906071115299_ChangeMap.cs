namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelMembers",
                c => new
                    {
                        MemberId = c.Guid(nullable: false),
                        IssueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberId, t.IssueId })
                .ForeignKey("dbo.ModelIssues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
            DropTable("dbo.ModelPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ModelPersons",
                c => new
                    {
                        PersonId = c.Guid(nullable: false),
                        IssueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.IssueId });
            
            DropForeignKey("dbo.ModelMembers", "IssueId", "dbo.ModelIssues");
            DropIndex("dbo.ModelMembers", new[] { "IssueId" });
            DropTable("dbo.ModelMembers");
        }
    }
}
