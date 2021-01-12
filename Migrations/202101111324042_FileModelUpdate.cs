namespace FlopBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Folders", "ParentFolder_Id", "dbo.Folders");
            DropIndex("dbo.Folders", new[] { "ParentFolder_Id" });
            AddColumn("dbo.Folders", "ParentFolder", c => c.Int());
            DropColumn("dbo.Folders", "ParentFolder_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Folders", "ParentFolder_Id", c => c.Int());
            DropColumn("dbo.Folders", "ParentFolder");
            CreateIndex("dbo.Folders", "ParentFolder_Id");
            AddForeignKey("dbo.Folders", "ParentFolder_Id", "dbo.Folders", "Id");
        }
    }
}
