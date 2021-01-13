namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distributors",
                c => new
                    {
                        DistributorId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Contact = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.DistributorId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 200),
                        BrandName = c.String(nullable: false, maxLength: 200),
                        Indication = c.String(nullable: false, maxLength: 200),
                        PackSize = c.String(maxLength: 100),
                        Expiry = c.DateTime(nullable: false),
                        StorageCondition = c.String(maxLength: 1000),
                        HasRestrictedIngredient = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ProductClassificationId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        DistributorId = c.Int(nullable: false),
                        addedBy = c.String(),
                        createdOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Distributors", t => t.DistributorId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ProductClassifications", t => t.ProductClassificationId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.RegionId)
                .Index(t => t.ProductClassificationId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.DistributorId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientsId = c.Int(nullable: false, identity: true),
                        CommonName = c.String(maxLength: 200),
                        INCIName = c.String(maxLength: 200),
                        Quantity = c.Double(nullable: false),
                        Unit = c.Int(nullable: false),
                        Restricted = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientsId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Contact = c.String(maxLength: 200),
                        Addresss = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductClassifications",
                c => new
                    {
                        ProductClassificationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ProductClassificationId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.RegionId);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        NationalId = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Products", "ProductClassificationId", "dbo.ProductClassifications");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Ingredients", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "DistributorId", "dbo.Distributors");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ingredients", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "DistributorId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "ProductClassificationId" });
            DropIndex("dbo.Products", new[] { "RegionId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Regions");
            DropTable("dbo.ProductClassifications");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Products");
            DropTable("dbo.Distributors");
        }
    }
}
