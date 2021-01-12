namespace FlopBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Folder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.Folder_Id)
                .Index(t => t.Folder_Id);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentFolder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.ParentFolder_Id)
                .Index(t => t.ParentFolder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Folders", "ParentFolder_Id", "dbo.Folders");
            DropForeignKey("dbo.Files", "Folder_Id", "dbo.Folders");
            DropIndex("dbo.Folders", new[] { "ParentFolder_Id" });
            DropIndex("dbo.Files", new[] { "Folder_Id" });
            DropTable("dbo.Folders");
            DropTable("dbo.Files");
        }
    }
}
