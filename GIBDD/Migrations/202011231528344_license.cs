namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class license : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TakeStrokes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TakeDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        DriverLicense_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverLicenses", t => t.DriverLicense_Id)
                .Index(t => t.DriverLicense_Id);
            
            AddColumn("dbo.DriverLicenses", "IsLicenseTaken", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TakeStrokes", "DriverLicense_Id", "dbo.DriverLicenses");
            DropIndex("dbo.TakeStrokes", new[] { "DriverLicense_Id" });
            DropColumn("dbo.DriverLicenses", "IsLicenseTaken");
            DropTable("dbo.TakeStrokes");
        }
    }
}
