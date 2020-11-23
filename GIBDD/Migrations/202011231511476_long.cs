namespace GIBDD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _long : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarOwners", "PassportData", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarOwners", "PassportData", c => c.Int(nullable: false));
        }
    }
}
