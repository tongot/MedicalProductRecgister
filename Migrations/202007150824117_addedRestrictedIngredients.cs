namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRestrictedIngredients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "RestrictedIngridients", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "RestrictedIngridients");
        }
    }
}
