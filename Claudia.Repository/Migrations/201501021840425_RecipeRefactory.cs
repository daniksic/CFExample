namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeRefactory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "LinkId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipes", "LinkId");
            AddForeignKey("dbo.Recipes", "LinkId", "dbo.Links", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "LinkId", "dbo.Links");
            DropIndex("dbo.Recipes", new[] { "LinkId" });
            DropColumn("dbo.Recipes", "LinkId");
        }
    }
}
