namespace ReferensApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        MeetingId = c.Int(nullable: false, identity: true),
                        Hour = c.Int(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        BookedBy = c.String(),
                    })
                .PrimaryKey(t => t.MeetingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meetings");
        }
    }
}
