namespace SupermarketChain.Data.DataContext.Migrations.SQLServer
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SupermarketChain.Measures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Abbreviation = c.String(nullable: false, maxLength: 25),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SupermarketChain.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 500),
                        MeasureId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SupermarketChain.Measures", t => t.MeasureId, cascadeDelete: true)
                .ForeignKey("SupermarketChain.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.MeasureId);
            
            CreateTable(
                "SupermarketChain.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SupermarketChain.Products", "VendorId", "SupermarketChain.Vendors");
            DropForeignKey("SupermarketChain.Products", "MeasureId", "SupermarketChain.Measures");
            DropIndex("SupermarketChain.Products", new[] { "MeasureId" });
            DropIndex("SupermarketChain.Products", new[] { "VendorId" });
            DropTable("SupermarketChain.Vendors");
            DropTable("SupermarketChain.Products");
            DropTable("SupermarketChain.Measures");
        }
    }
}
