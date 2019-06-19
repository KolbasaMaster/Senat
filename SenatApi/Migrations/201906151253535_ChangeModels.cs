namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ModelMeetings", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ModelMeetings", "Date", c => c.String());
        }
    }
}
