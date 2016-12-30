namespace ObajuStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeleteColToTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationRoles", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Brands", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Posts", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Slides", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Vehicles", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Wishlists", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wishlists", "IsDeleted");
            DropColumn("dbo.Vehicles", "IsDeleted");
            DropColumn("dbo.Slides", "IsDeleted");
            DropColumn("dbo.Posts", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Orders", "IsDeleted");
            DropColumn("dbo.Brands", "IsDeleted");
            DropColumn("dbo.ApplicationRoles", "IsDeleted");
        }
    }
}
