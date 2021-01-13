namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsUniqueToDestributors : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Distributors", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Distributors", new[] { "Name" });
        }
    }
}
