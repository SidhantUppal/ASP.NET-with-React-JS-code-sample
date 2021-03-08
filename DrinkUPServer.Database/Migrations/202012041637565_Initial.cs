namespace DrinkUPServer.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boosts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Target = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Title = c.String(),
                        SubTitle = c.String(),
                        Image = c.String(),
                        Price = c.Int(nullable: false),
                        Ingredients = c.String(),
                        Details = c.String(),
                        Machine_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.Machine_Id)
                .Index(t => t.Machine_Id);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Location = c.String(),
                        InitializationToken = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Target = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        Title = c.String(),
                        Image = c.String(),
                        Capacity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Machine_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.Machine_Id)
                .Index(t => t.Machine_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Total = c.Double(nullable: false),
                        Boost_Id = c.String(maxLength: 128),
                        Machine_Id = c.String(maxLength: 128),
                        Size_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boosts", t => t.Boost_Id)
                .ForeignKey("dbo.Machines", t => t.Machine_Id)
                .ForeignKey("dbo.Sizes", t => t.Size_Id)
                .Index(t => t.Boost_Id)
                .Index(t => t.Machine_Id)
                .Index(t => t.Size_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Size_Id", "dbo.Sizes");
            DropForeignKey("dbo.Transactions", "Machine_Id", "dbo.Machines");
            DropForeignKey("dbo.Transactions", "Boost_Id", "dbo.Boosts");
            DropForeignKey("dbo.Sizes", "Machine_Id", "dbo.Machines");
            DropForeignKey("dbo.Boosts", "Machine_Id", "dbo.Machines");
            DropIndex("dbo.Transactions", new[] { "Size_Id" });
            DropIndex("dbo.Transactions", new[] { "Machine_Id" });
            DropIndex("dbo.Transactions", new[] { "Boost_Id" });
            DropIndex("dbo.Sizes", new[] { "Machine_Id" });
            DropIndex("dbo.Boosts", new[] { "Machine_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Sizes");
            DropTable("dbo.Customers");
            DropTable("dbo.Machines");
            DropTable("dbo.Boosts");
        }
    }
}
