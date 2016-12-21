namespace ObajuStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddColIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationUsers", "IsViewed");
            // Add this line
            RenameColumn("dbo.ApplicationUsers", "IsViewed", "IsDeleted");
        }

        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "IsViewed", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationUsers", "IsDeleted");
            RenameColumn("dbo.ApplicationUsers", "IsDelete", "IsViewed");
        }
    }
}
