namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewContext1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "MemberSince", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean());
            AlterColumn("dbo.AspNetUsers", "MemberSince", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Rating", c => c.Int());
        }
    }
}
