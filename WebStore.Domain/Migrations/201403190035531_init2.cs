namespace WebStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryID_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "CustomerID_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.OrderItems", "ProductID_ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "CustomerID_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.OrderItems", "OrderID_OrderID", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "CategoryID_CategoryID" });
            DropIndex("dbo.Products", new[] { "CustomerID_CustomerID" });
            DropIndex("dbo.OrderItems", new[] { "ProductID_ProductID" });
            DropIndex("dbo.Orders", new[] { "CustomerID_CustomerID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID_OrderID" });
            RenameColumn(table: "dbo.Addresses", name: "CustomerID_CustomerID", newName: "CustomerAddress_CustomerID");
            AddColumn("dbo.Orders", "CustomerOrder_CustomerID", c => c.Int());
            AddColumn("dbo.OrderItems", "OrderItemOrder_OrderID", c => c.Int());
            AddColumn("dbo.OrderItems", "OrderItemProduct_ProductID", c => c.Int());
            AddColumn("dbo.Products", "ProductCategory_CategoryID", c => c.Int());
            AddColumn("dbo.Products", "ProductCustomer_CustomerID", c => c.Int());
            CreateIndex("dbo.Products", "ProductCategory_CategoryID");
            CreateIndex("dbo.Products", "ProductCustomer_CustomerID");
            CreateIndex("dbo.OrderItems", "OrderItemProduct_ProductID");
            CreateIndex("dbo.Orders", "CustomerOrder_CustomerID");
            CreateIndex("dbo.OrderItems", "OrderItemOrder_OrderID");
            AddForeignKey("dbo.Products", "ProductCategory_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Products", "ProductCustomer_CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.OrderItems", "OrderItemProduct_ProductID", "dbo.Products", "ProductID");
            AddForeignKey("dbo.Orders", "CustomerOrder_CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.OrderItems", "OrderItemOrder_OrderID", "dbo.Orders", "OrderID");
            DropColumn("dbo.OrderItems", "OrderID_OrderID");
            DropColumn("dbo.OrderItems", "ProductID_ProductID");
            DropColumn("dbo.Products", "CategoryID_CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryID_CategoryID", c => c.Int());
            AddColumn("dbo.OrderItems", "ProductID_ProductID", c => c.Int());
            AddColumn("dbo.OrderItems", "OrderID_OrderID", c => c.Int());
            DropForeignKey("dbo.OrderItems", "OrderItemOrder_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerOrder_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.OrderItems", "OrderItemProduct_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCustomer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "ProductCategory_CategoryID", "dbo.Categories");
            DropIndex("dbo.OrderItems", new[] { "OrderItemOrder_OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerOrder_CustomerID" });
            DropIndex("dbo.OrderItems", new[] { "OrderItemProduct_ProductID" });
            DropIndex("dbo.Products", new[] { "ProductCustomer_CustomerID" });
            DropIndex("dbo.Products", new[] { "ProductCategory_CategoryID" });
            DropColumn("dbo.Products", "ProductCustomer_CustomerID");
            DropColumn("dbo.Products", "ProductCategory_CategoryID");
            DropColumn("dbo.OrderItems", "OrderItemProduct_ProductID");
            DropColumn("dbo.OrderItems", "OrderItemOrder_OrderID");
            DropColumn("dbo.Orders", "CustomerOrder_CustomerID");
            RenameColumn(table: "dbo.Addresses", name: "CustomerAddress_CustomerID", newName: "CustomerID_CustomerID");
            CreateIndex("dbo.OrderItems", "OrderID_OrderID");
            CreateIndex("dbo.Orders", "CustomerID_CustomerID");
            CreateIndex("dbo.OrderItems", "ProductID_ProductID");
            CreateIndex("dbo.Products", "CustomerID_CustomerID");
            CreateIndex("dbo.Products", "CategoryID_CategoryID");
            AddForeignKey("dbo.OrderItems", "OrderID_OrderID", "dbo.Orders", "OrderID");
            AddForeignKey("dbo.Orders", "CustomerID_CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.OrderItems", "ProductID_ProductID", "dbo.Products", "ProductID");
            AddForeignKey("dbo.Products", "CustomerID_CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.Products", "CategoryID_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
