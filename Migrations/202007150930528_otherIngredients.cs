namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherIngredients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OtherIngredients", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OtherIngredients");
        }
    }
}
