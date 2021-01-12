namespace FlopBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FolderModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Folder_Id", "dbo.Folders");
            DropIndex("dbo.Files", new[] { "Folder_Id" });
            AddColumn("dbo.Files", "Folder", c => c.Int());
            DropColumn("dbo.Files", "Folder_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Folder_Id", c => c.Int());
            DropColumn("dbo.Files", "Folder");
            CreateIndex("dbo.Files", "Folder_Id");
            AddForeignKey("dbo.Files", "Folder_Id", "dbo.Folders", "Id");
        }
    }
}
