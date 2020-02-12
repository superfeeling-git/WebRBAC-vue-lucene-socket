namespace WebRBAC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class history1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.sysClassRoles", "sysClass_ClassID", "dbo.sysClass");
            DropForeignKey("dbo.sysClassRoles", "Roles_RoleID", "dbo.Roles");
            DropIndex("dbo.sysClassRoles", new[] { "sysClass_ClassID" });
            DropIndex("dbo.sysClassRoles", new[] { "Roles_RoleID" });
            CreateTable(
                "dbo.RolesClass",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.ClassID })
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.sysClass", t => t.ClassID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.ClassID);
            
            DropTable("dbo.sysClassRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.sysClassRoles",
                c => new
                    {
                        sysClass_ClassID = c.Int(nullable: false),
                        Roles_RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.sysClass_ClassID, t.Roles_RoleID });
            
            DropForeignKey("dbo.RolesClass", "ClassID", "dbo.sysClass");
            DropForeignKey("dbo.RolesClass", "RoleID", "dbo.Roles");
            DropIndex("dbo.RolesClass", new[] { "ClassID" });
            DropIndex("dbo.RolesClass", new[] { "RoleID" });
            DropTable("dbo.RolesClass");
            CreateIndex("dbo.sysClassRoles", "Roles_RoleID");
            CreateIndex("dbo.sysClassRoles", "sysClass_ClassID");
            AddForeignKey("dbo.sysClassRoles", "Roles_RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            AddForeignKey("dbo.sysClassRoles", "sysClass_ClassID", "dbo.sysClass", "ClassID", cascadeDelete: true);
        }
    }
}
