namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForumIdinPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "MyForum_Id", "dbo.MyForums");
            DropIndex("dbo.Posts", new[] { "MyForum_Id" });
            RenameColumn(table: "dbo.Posts", name: "MyForum_Id", newName: "MyForumId");
            AlterColumn("dbo.Posts", "MyForumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "MyForumId");
            AddForeignKey("dbo.Posts", "MyForumId", "dbo.MyForums", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "MyForumId", "dbo.MyForums");
            DropIndex("dbo.Posts", new[] { "MyForumId" });
            AlterColumn("dbo.Posts", "MyForumId", c => c.Int());
            RenameColumn(table: "dbo.Posts", name: "MyForumId", newName: "MyForum_Id");
            CreateIndex("dbo.Posts", "MyForum_Id");
            AddForeignKey("dbo.Posts", "MyForum_Id", "dbo.MyForums", "Id");
        }
    }
}
