namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWidgets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Widgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        WidgetName = c.String(),
                        SubWidgetName = c.String(),
                        ReferenceType = c.Int(nullable: false),
                        ReferenceId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Links", "Title", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "Title", c => c.String(maxLength: 40));
            DropTable("dbo.Widgets");
        }
    }
}
