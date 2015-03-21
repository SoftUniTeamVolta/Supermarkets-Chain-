namespace SupermarketChain.Data.DataContext.Migrations.Oracle
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ADMIN.MEASURES",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NAME = c.String(nullable: false, maxLength: 100),
                        ABBREVIATION = c.String(nullable: false, maxLength: 25),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DELETED_ON = c.DateTime(),
                        CREATED_ON = c.DateTime(nullable: false),
                        MODIFIED_ON = c.DateTime(),
                        PRESERVE_CREATED_ON = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ADMIN.PRODUCTS",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        VENDOR_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAME = c.String(nullable: false, maxLength: 500),
                        MEASURE_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PRICE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DELETED_ON = c.DateTime(),
                        CREATED_ON = c.DateTime(nullable: false),
                        MODIFIED_ON = c.DateTime(),
                        PRESERVE_CREATED_ON = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ADMIN.MEASURES", t => t.MEASURE_ID, cascadeDelete: true)
                .ForeignKey("ADMIN.VENDORS", t => t.VENDOR_ID, cascadeDelete: true)
                .Index(t => t.VENDOR_ID)
                .Index(t => t.MEASURE_ID);
            
            CreateTable(
                "ADMIN.VENDORS",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NAME = c.String(nullable: false, maxLength: 250),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DELETED_ON = c.DateTime(),
                        CREATED_ON = c.DateTime(nullable: false),
                        MODIFIED_ON = c.DateTime(),
                        PRESERVE_CREATED_ON = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "ADMIN.SALES",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATE = c.DateTime(nullable: false),
                        PRODUCT_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        QUANTITY = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UNIT_PRICE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUPERMARKET_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DELETED_ON = c.DateTime(),
                        CREATED_ON = c.DateTime(nullable: false),
                        MODIFIED_ON = c.DateTime(),
                        PRESERVE_CREATED_ON = c.Decimal(nullable: false, precision: 1, scale: 0),
                        VENDOR_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("ADMIN.PRODUCTS", t => t.PRODUCT_ID, cascadeDelete: true)
                .ForeignKey("ADMIN.SUPERMARKETS", t => t.SUPERMARKET_ID, cascadeDelete: true)
                .ForeignKey("ADMIN.VENDORS", t => t.VENDOR_ID, cascadeDelete: true)
                .Index(t => t.PRODUCT_ID)
                .Index(t => t.SUPERMARKET_ID)
                .Index(t => t.VENDOR_ID);
            
            CreateTable(
                "ADMIN.SUPERMARKETS",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NAME = c.String(nullable: false, maxLength: 100),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DELETED_ON = c.DateTime(),
                        CREATED_ON = c.DateTime(nullable: false),
                        MODIFIED_ON = c.DateTime(),
                        PRESERVE_CREATED_ON = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ADMIN.PRODUCTS", "VENDOR_ID", "ADMIN.VENDORS");
            DropForeignKey("ADMIN.SALES", "VENDOR_ID", "ADMIN.VENDORS");
            DropForeignKey("ADMIN.SALES", "SUPERMARKET_ID", "ADMIN.SUPERMARKETS");
            DropForeignKey("ADMIN.SALES", "PRODUCT_ID", "ADMIN.PRODUCTS");
            DropForeignKey("ADMIN.PRODUCTS", "MEASURE_ID", "ADMIN.MEASURES");
            DropIndex("ADMIN.SALES", new[] { "VENDOR_ID" });
            DropIndex("ADMIN.SALES", new[] { "SUPERMARKET_ID" });
            DropIndex("ADMIN.SALES", new[] { "PRODUCT_ID" });
            DropIndex("ADMIN.PRODUCTS", new[] { "MEASURE_ID" });
            DropIndex("ADMIN.PRODUCTS", new[] { "VENDOR_ID" });
            DropTable("ADMIN.SUPERMARKETS");
            DropTable("ADMIN.SALES");
            DropTable("ADMIN.VENDORS");
            DropTable("ADMIN.PRODUCTS");
            DropTable("ADMIN.MEASURES");
        }
    }
}
