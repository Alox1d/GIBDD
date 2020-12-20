using System.Data.Entity.Migrations;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleOffenses", "Number", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleOffenses", "Number", c => c.Int(nullable: false));
        }
    }
}
