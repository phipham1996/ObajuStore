namespace ObajuStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBioToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Bio", c => c.String(maxLength: 700));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Bio");
        }
    }
}
