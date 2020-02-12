namespace WebRBAC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class history : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        AddTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.sysClass",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        ParentID = c.Int(nullable: false),
                        ParentPath = c.String(),
                        Depth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassID);
            
            CreateTable(
                "dbo.sysClassRoles",
                c => new
                    {
                        sysClass_ClassID = c.Int(nullable: false),
                        Roles_RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.sysClass_ClassID, t.Roles_RoleID })
                .ForeignKey("dbo.sysClass", t => t.sysClass_ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_RoleID, cascadeDelete: true)
                .Index(t => t.sysClass_ClassID)
                .Index(t => t.Roles_RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sysClassRoles", "Roles_RoleID", "dbo.Roles");
            DropForeignKey("dbo.sysClassRoles", "sysClass_ClassID", "dbo.sysClass");
            DropIndex("dbo.sysClassRoles", new[] { "Roles_RoleID" });
            DropIndex("dbo.sysClassRoles", new[] { "sysClass_ClassID" });
            DropTable("dbo.sysClassRoles");
            DropTable("dbo.sysClass");
            DropTable("dbo.Roles");
        }
    }
}
