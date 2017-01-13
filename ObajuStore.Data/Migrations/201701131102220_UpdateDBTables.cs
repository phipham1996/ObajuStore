namespace ObajuStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDBTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrackOrders", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TrackOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.TrackOrders", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.TrackOrders", new[] { "OrderId" });
            DropIndex("dbo.TrackOrders", new[] { "VehicleId" });
            DropIndex("dbo.TrackOrders", new[] { "UserId" });
            DropTable("dbo.TrackOrders");
            DropTable("dbo.Vehicles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VehicleNumber = c.String(maxLength: 30, unicode: false),
                        DriverName = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        ModelID = c.String(maxLength: 50),
                        Model = c.String(maxLength: 150),
                        Description = c.String(),
                        IsDeleted = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        MetaKeyword = c.String(maxLength: 150),
                        MetaDescription = c.String(maxLength: 150),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrackOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.TrackOrders", "UserId");
            CreateIndex("dbo.TrackOrders", "VehicleId");
            CreateIndex("dbo.TrackOrders", "OrderId");
            AddForeignKey("dbo.TrackOrders", "VehicleId", "dbo.Vehicles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TrackOrders", "OrderId", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TrackOrders", "UserId", "dbo.ApplicationUsers", "Id");
        }
    }
}
