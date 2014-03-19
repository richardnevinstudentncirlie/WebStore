namespace WebStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        StreetLine1 = c.String(nullable: false, maxLength: 50),
                        StreetLine2 = c.String(maxLength: 50),
                        StreetLine3 = c.String(maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(nullable: false, maxLength: 50),
                        County = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CustomerID_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Customers", t => t.CustomerID_CustomerID)
                .Index(t => t.CustomerID_CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Company = c.String(nullable: false, maxLength: 120),
                        Email = c.String(maxLength: 225),
                        Phone = c.String(maxLength: 15),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        TotalOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCount = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        BillToAddress_AddressID = c.Int(),
                        CustomerID_CustomerID = c.Int(),
                        ShipToAddress_AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Addresses", t => t.BillToAddress_AddressID)
                .ForeignKey("dbo.Customers", t => t.CustomerID_CustomerID)
                .ForeignKey("dbo.Addresses", t => t.ShipToAddress_AddressID)
                .Index(t => t.BillToAddress_AddressID)
                .Index(t => t.CustomerID_CustomerID)
                .Index(t => t.ShipToAddress_AddressID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false, maxLength: 50),
                        ImageURL = c.String(nullable: false, maxLength: 255),
                        Special = c.Boolean(nullable: false),
                        Seller = c.Int(nullable: false),
                        Buyer = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        OrderID_OrderID = c.Int(),
                        ProductID_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Orders", t => t.OrderID_OrderID)
                .ForeignKey("dbo.Products", t => t.ProductID_ProductID)
                .Index(t => t.OrderID_OrderID)
                .Index(t => t.ProductID_ProductID);
            
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ImageURL", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Products", "Special", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Seller", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Buyer", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CategoryID_CategoryID", c => c.Int());
            AddColumn("dbo.Products", "CustomerID_CustomerID", c => c.Int());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Products", "CategoryID_CategoryID");
            CreateIndex("dbo.Products", "CustomerID_CustomerID");
            AddForeignKey("dbo.Products", "CategoryID_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Products", "CustomerID_CustomerID", "dbo.Customers", "CustomerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShipToAddress_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.OrderItems", "ProductID_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CustomerID_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryID_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.OrderItems", "OrderID_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerID_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "BillToAddress_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CustomerID_CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "ShipToAddress_AddressID" });
            DropIndex("dbo.OrderItems", new[] { "ProductID_ProductID" });
            DropIndex("dbo.Products", new[] { "CustomerID_CustomerID" });
            DropIndex("dbo.Products", new[] { "CategoryID_CategoryID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID_OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerID_CustomerID" });
            DropIndex("dbo.Orders", new[] { "BillToAddress_AddressID" });
            DropIndex("dbo.Addresses", new[] { "CustomerID_CustomerID" });
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "CustomerID_CustomerID");
            DropColumn("dbo.Products", "CategoryID_CategoryID");
            DropColumn("dbo.Products", "UpdatedAt");
            DropColumn("dbo.Products", "CreatedAt");
            DropColumn("dbo.Products", "Buyer");
            DropColumn("dbo.Products", "Seller");
            DropColumn("dbo.Products", "Special");
            DropColumn("dbo.Products", "ImageURL");
            DropColumn("dbo.Products", "Quantity");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
