namespace ReferensApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeHour : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meetings", "Hour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "Hour", c => c.Int(nullable: false));
        }
    }
}
