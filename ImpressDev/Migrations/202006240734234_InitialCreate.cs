namespace ImpressDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        SubTitle = c.String(),
                        Level = c.String(),
                        PublishingHouse = c.String(),
                        Cover = c.String(),
                        Pages = c.String(),
                        Description = c.String(),
                        PhotoSource = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bestseller = c.Boolean(nullable: false),
                        Inaccessible = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropIndex("dbo.OrderItem", new[] { "BookId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropTable("dbo.Order");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
        }
    }
}
