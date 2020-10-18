namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.PostReplies", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Posts", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.PostReplies", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PostReplies", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.PostReplies", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Posts", name: "UserId", newName: "User_Id");
        }
    }
}
