namespace Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Version = c.String(maxLength: 20),
                        Size = c.Long(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        ProductCategory = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductName);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 40),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Url = c.String(maxLength: 200),
                        VendorContact = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductReleaseDates",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 40),
                        RealeseOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductReleaseDates");
            DropTable("dbo.Products");
            DropTable("dbo.Drivers");
        }
    }
}
