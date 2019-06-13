namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableInvitees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelPersons",
                c => new
                    {
                        PersonId = c.Guid(nullable: false),
                        IssueId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.IssueId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ModelPersons");
        }
    }
}
