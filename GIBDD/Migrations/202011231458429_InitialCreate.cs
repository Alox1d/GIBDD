namespace GIBDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleOffenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Description = c.String(),
                        Penalty = c.String(),
                        TakeLicenseTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleOffenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Penalty = c.Double(),
                        Address = c.String(),
                        TakeLicenseTime = c.Int(),
                        ArticleOffense_Id = c.Int(),
                        CarOwner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleOffenses", t => t.ArticleOffense_Id)
                .ForeignKey("dbo.CarOwners", t => t.CarOwner_Id)
                .Index(t => t.ArticleOffense_Id)
                .Index(t => t.CarOwner_Id);
            
            CreateTable(
                "dbo.CarOwners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        PassportData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriverLicenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReleaseDate = c.DateTime(nullable: false),
                        CarOwner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarOwners", t => t.CarOwner_Id)
                .Index(t => t.CarOwner_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        MaintenanceDate = c.DateTime(),
                        MaintenanceSuccess = c.Boolean(nullable: false),
                        CarOwner_Id = c.Int(),
                        Color_Id = c.Int(),
                        DriverLicense_Id = c.Int(),
                        Model_Id = c.Int(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarOwners", t => t.CarOwner_Id)
                .ForeignKey("dbo.Colors", t => t.Color_Id)
                .ForeignKey("dbo.DriverLicenses", t => t.DriverLicense_Id)
                .ForeignKey("dbo.Models", t => t.Model_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.CarOwner_Id)
                .Index(t => t.Color_Id)
                .Index(t => t.DriverLicense_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryDriverLicenses",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        DriverLicense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.DriverLicense_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.DriverLicenses", t => t.DriverLicense_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.DriverLicense_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleOffenses", "CarOwner_Id", "dbo.CarOwners");
            DropForeignKey("dbo.Vehicles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Vehicles", "Model_Id", "dbo.Models");
            DropForeignKey("dbo.Vehicles", "DriverLicense_Id", "dbo.DriverLicenses");
            DropForeignKey("dbo.Vehicles", "Color_Id", "dbo.Colors");
            DropForeignKey("dbo.Vehicles", "CarOwner_Id", "dbo.CarOwners");
            DropForeignKey("dbo.CategoryDriverLicenses", "DriverLicense_Id", "dbo.DriverLicenses");
            DropForeignKey("dbo.CategoryDriverLicenses", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.DriverLicenses", "CarOwner_Id", "dbo.CarOwners");
            DropForeignKey("dbo.VehicleOffenses", "ArticleOffense_Id", "dbo.ArticleOffenses");
            DropIndex("dbo.CategoryDriverLicenses", new[] { "DriverLicense_Id" });
            DropIndex("dbo.CategoryDriverLicenses", new[] { "Category_Id" });
            DropIndex("dbo.Vehicles", new[] { "Category_Id" });
            DropIndex("dbo.Vehicles", new[] { "Model_Id" });
            DropIndex("dbo.Vehicles", new[] { "DriverLicense_Id" });
            DropIndex("dbo.Vehicles", new[] { "Color_Id" });
            DropIndex("dbo.Vehicles", new[] { "CarOwner_Id" });
            DropIndex("dbo.DriverLicenses", new[] { "CarOwner_Id" });
            DropIndex("dbo.VehicleOffenses", new[] { "CarOwner_Id" });
            DropIndex("dbo.VehicleOffenses", new[] { "ArticleOffense_Id" });
            DropTable("dbo.CategoryDriverLicenses");
            DropTable("dbo.Models");
            DropTable("dbo.Colors");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Categories");
            DropTable("dbo.DriverLicenses");
            DropTable("dbo.CarOwners");
            DropTable("dbo.VehicleOffenses");
            DropTable("dbo.ArticleOffenses");
        }
    }
}
