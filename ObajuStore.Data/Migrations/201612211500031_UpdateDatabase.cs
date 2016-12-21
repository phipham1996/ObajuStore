namespace ObajuStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationUsers", "IsViewed");
        }

        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "IsViewed", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationUsers", "IsDeleted");
        }
    }
}
