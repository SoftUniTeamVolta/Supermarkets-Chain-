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
                        IsDeleted = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Decimal(nullable: false, precision: 1, scale: 0),
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
                        DATE = c.DateTime(nullable: false),
                        IsDeleted = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Decimal(nullable: false, precision: 1, scale: 0),
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
                        IsDeleted = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PreserveCreatedOn = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ADMIN.PRODUCTS", "VENDOR_ID", "ADMIN.VENDORS");
            DropForeignKey("ADMIN.PRODUCTS", "MEASURE_ID", "ADMIN.MEASURES");
            DropIndex("ADMIN.PRODUCTS", new[] { "MEASURE_ID" });
            DropIndex("ADMIN.PRODUCTS", new[] { "VENDOR_ID" });
            DropTable("ADMIN.VENDORS");
            DropTable("ADMIN.PRODUCTS");
            DropTable("ADMIN.MEASURES");
        }
    }
}
