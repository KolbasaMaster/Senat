namespace NewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscriminator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelMeetings", "Discriminator", c => c.String());
            DropColumn("dbo.ModelMeetings", "_discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelMeetings", "_discriminator", c => c.String());
            DropColumn("dbo.ModelMeetings", "Discriminator");
        }
    }
}
