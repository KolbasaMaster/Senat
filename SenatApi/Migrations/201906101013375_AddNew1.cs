namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNew1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelIssues", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ModelMeetings", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModelMeetings", "Status");
            DropColumn("dbo.ModelIssues", "Status");
        }
    }
}
