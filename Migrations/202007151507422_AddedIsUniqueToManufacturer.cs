namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsUniqueToManufacturer : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Manufacturers", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Manufacturers", new[] { "Name" });
        }
    }
}
