namespace WebStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        CustomerAddress_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Customers", t => t.CustomerAddress_CustomerID)
                .Index(t => t.CustomerAddress_CustomerID);
            
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
                        CustomerOrder_CustomerID = c.Int(),
                        ShipToAddress_AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Addresses", t => t.BillToAddress_AddressID)
                .ForeignKey("dbo.Customers", t => t.CustomerOrder_CustomerID)
                .ForeignKey("dbo.Addresses", t => t.ShipToAddress_AddressID)
                .Index(t => t.BillToAddress_AddressID)
                .Index(t => t.CustomerOrder_CustomerID)
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
                        Special = c.Boolean(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Seller = c.Int(nullable: false),
                        Buyer = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        OrderItemOrder_OrderID = c.Int(),
                        OrderItemProduct_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Orders", t => t.OrderItemOrder_OrderID)
                .ForeignKey("dbo.Products", t => t.OrderItemProduct_ProductID)
                .Index(t => t.OrderItemOrder_OrderID)
                .Index(t => t.OrderItemProduct_ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Special = c.Boolean(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Seller = c.Int(nullable: false),
                        Buyer = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ProductCategory_CategoryID = c.Int(),
                        ProductCustomer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.ProductCategory_CategoryID)
                .ForeignKey("dbo.Customers", t => t.ProductCustomer_CustomerID)
                .Index(t => t.ProductCategory_CategoryID)
                .Index(t => t.ProductCustomer_CustomerID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryCode = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShipToAddress_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.OrderItems", "OrderItemProduct_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCustomer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "ProductCategory_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.OrderItems", "OrderItemOrder_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerOrder_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "BillToAddress_AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CustomerAddress_CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "ShipToAddress_AddressID" });
            DropIndex("dbo.OrderItems", new[] { "OrderItemProduct_ProductID" });
            DropIndex("dbo.Products", new[] { "ProductCustomer_CustomerID" });
            DropIndex("dbo.Products", new[] { "ProductCategory_CategoryID" });
            DropIndex("dbo.OrderItems", new[] { "OrderItemOrder_OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerOrder_CustomerID" });
            DropIndex("dbo.Orders", new[] { "BillToAddress_AddressID" });
            DropIndex("dbo.Addresses", new[] { "CustomerAddress_CustomerID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
