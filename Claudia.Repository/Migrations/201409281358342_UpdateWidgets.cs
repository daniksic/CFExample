namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWidgets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Widgets", "Description", c => c.String());
            AlterColumn("dbo.Widgets", "ReferenceType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Widgets", "ReferenceType", c => c.Int(nullable: false));
            DropColumn("dbo.Widgets", "Description");
        }
    }
}
