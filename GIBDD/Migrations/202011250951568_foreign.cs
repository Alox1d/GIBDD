namespace GIBDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreign : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "Color_Id", "dbo.Colors");
            DropIndex("dbo.Vehicles", new[] { "Color_Id" });
            AddColumn("dbo.Vehicles", "Color_Id1", c => c.Int());
            AlterColumn("dbo.Vehicles", "Color_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "Color_Id1");
            AddForeignKey("dbo.Vehicles", "Color_Id1", "dbo.Colors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Color_Id1", "dbo.Colors");
            DropIndex("dbo.Vehicles", new[] { "Color_Id1" });
            AlterColumn("dbo.Vehicles", "Color_Id", c => c.Int());
            DropColumn("dbo.Vehicles", "Color_Id1");
            CreateIndex("dbo.Vehicles", "Color_Id");
            AddForeignKey("dbo.Vehicles", "Color_Id", "dbo.Colors", "Id");
        }
    }
}
