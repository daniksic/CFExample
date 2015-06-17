namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinksUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkId = c.Int(nullable: false),
                        ServerFileName = c.String(),
                        ClientLocalFileName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Links", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.LinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinksUrls", "LinkId", "dbo.Links");
            DropIndex("dbo.LinksUrls", new[] { "LinkId" });
            DropTable("dbo.LinksUrls");
        }
    }
}
