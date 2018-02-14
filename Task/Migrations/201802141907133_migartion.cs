namespace Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migartion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Size = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Version = c.String(nullable: false, maxLength: 20),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        ProductCategory = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Size);
            
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
                        RealeseOn = c.String(nullable: false, maxLength: 30),
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
