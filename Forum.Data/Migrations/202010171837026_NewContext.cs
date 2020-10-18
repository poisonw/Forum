namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "MemberSince", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "MemberSince", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
    }
}
