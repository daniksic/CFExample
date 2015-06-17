namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCheckColumnToLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "ClientLocalFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "ClientLocalFileName");
        }
    }
}
