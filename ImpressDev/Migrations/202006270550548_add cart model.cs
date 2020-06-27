namespace ImpressDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcartmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItem", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItem", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItem", "Quantity");
        }
    }
}
