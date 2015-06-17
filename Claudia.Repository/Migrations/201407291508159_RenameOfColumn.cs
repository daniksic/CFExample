namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameOfColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "ServerFileName", c => c.String());
            DropColumn("dbo.Links", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Links", "Url", c => c.String());
            DropColumn("dbo.Links", "ServerFileName");
        }
    }
}
