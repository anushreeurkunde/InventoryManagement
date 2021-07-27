namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRoles", "Role", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRoles", "Role", c => c.Int(nullable: false));
        }
    }
}
