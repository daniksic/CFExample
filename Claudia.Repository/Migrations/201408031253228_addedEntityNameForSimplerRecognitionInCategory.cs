namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEntityNameForSimplerRecognitionInCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObjectCategories", "EntityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ObjectCategories", "EntityName");
        }
    }
}
