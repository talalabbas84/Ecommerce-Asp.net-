namespace EcommerceDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRolesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRoles");
        }
    }
}
