namespace Claudia.Repository.Migrations.v3
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AskClaudias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        Answer = c.String(),
                        AnswerDate = c.DateTime(nullable: false),
                        IsAnswered = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastLogInDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        EntityListId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityLists", t => t.EntityListId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.EntityListId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EntityLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.EntityCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        EntityName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServerFileName = c.String(),
                        ClientLocalFileName = c.String(),
                        EntityListId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityLists", t => t.EntityListId, cascadeDelete: true)
                .Index(t => t.EntityListId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RatingScore = c.Int(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        EntityListId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityLists", t => t.EntityListId, cascadeDelete: true)
                .Index(t => t.EntityListId);
            
            CreateTable(
                "dbo.RecipeCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(),
                        PrepTime = c.String(),
                        Ingredients = c.String(),
                        Directions = c.String(),
                        Serves = c.Int(nullable: false),
                        EntityListId = c.Int(nullable: false),
                        RecipeCategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityLists", t => t.EntityListId, cascadeDelete: true)
                .ForeignKey("dbo.RecipeCategories", t => t.RecipeCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.EntityListId)
                .Index(t => t.RecipeCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Widgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        WidgetName = c.String(),
                        SubWidgetName = c.String(),
                        EntityCategoryId = c.Int(nullable: false),
                        EntityListId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityCategories", t => t.EntityCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.EntityLists", t => t.EntityListId, cascadeDelete: true)
                .Index(t => t.EntityCategoryId)
                .Index(t => t.EntityListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Widgets", "EntityListId", "dbo.EntityLists");
            DropForeignKey("dbo.Widgets", "EntityCategoryId", "dbo.EntityCategories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recipes", "RecipeCategoryId", "dbo.RecipeCategories");
            DropForeignKey("dbo.Recipes", "EntityListId", "dbo.EntityLists");
            DropForeignKey("dbo.Ratings", "EntityListId", "dbo.EntityLists");
            DropForeignKey("dbo.Links", "EntityListId", "dbo.EntityLists");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "EntityListId", "dbo.EntityLists");
            DropForeignKey("dbo.EntityLists", "CategoryId", "dbo.EntityCategories");
            DropForeignKey("dbo.AskClaudias", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Widgets", new[] { "EntityListId" });
            DropIndex("dbo.Widgets", new[] { "EntityCategoryId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropIndex("dbo.Recipes", new[] { "RecipeCategoryId" });
            DropIndex("dbo.Recipes", new[] { "EntityListId" });
            DropIndex("dbo.Ratings", new[] { "EntityListId" });
            DropIndex("dbo.Links", new[] { "EntityListId" });
            DropIndex("dbo.EntityLists", new[] { "CategoryId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "EntityListId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AskClaudias", new[] { "UserId" });
            DropTable("dbo.Widgets");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeCategories");
            DropTable("dbo.Ratings");
            DropTable("dbo.Links");
            DropTable("dbo.EntityCategories");
            DropTable("dbo.EntityLists");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AskClaudias");
        }
    }
}
