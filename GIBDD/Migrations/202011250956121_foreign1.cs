namespace GIBDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreign1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Vehicles", new[] { "Color_Id1" });
            DropColumn("dbo.Vehicles", "Color_Id");
            RenameColumn(table: "dbo.Vehicles", name: "Color_Id1", newName: "Color_Id");
            AlterColumn("dbo.Vehicles", "Color_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "Color_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vehicles", new[] { "Color_Id" });
            AlterColumn("dbo.Vehicles", "Color_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Vehicles", name: "Color_Id", newName: "Color_Id1");
            AddColumn("dbo.Vehicles", "Color_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "Color_Id1");
        }
    }
}
