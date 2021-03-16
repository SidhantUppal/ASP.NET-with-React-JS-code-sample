namespace DrinkUPServer.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boosts", "Machine_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Boosts", "Machine_Id");
            AddForeignKey("dbo.Boosts", "Machine_Id", "dbo.Machines", "Id");
            DropTable("dbo.MachineBoostjns");
            DropTable("dbo.MachineSizejns");
            DropTable("dbo.Media");
            DropTable("dbo.PaymentAddresses");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.StripePaymentDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StripePaymentDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Long(nullable: false),
                        BalanceTransactionId = c.String(),
                        CustomerId = c.String(),
                        ChargeID = c.String(),
                        Paymentmethod = c.String(),
                        Status = c.String(),
                        PaymentDate = c.DateTime(nullable: false),
                        RefundID = c.String(),
                        PaymentType = c.String(),
                        PaymentID = c.String(),
                        AmountRefunded = c.String(),
                        RefundDate = c.DateTime(),
                        TranID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TranID = c.String(),
                        Cancelled = c.Boolean(nullable: false),
                        email = c.String(),
                        Paid = c.Boolean(nullable: false),
                        PayerID = c.String(),
                        PaymentID = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        PaymentToken = c.String(),
                        Total = c.Double(nullable: false),
                        RefundID = c.String(),
                        RefundDate = c.DateTime(),
                        BillingID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentAddresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentID = c.String(),
                        city = c.String(),
                        country_code = c.String(),
                        line1 = c.String(),
                        line2 = c.String(),
                        postal_code = c.String(),
                        recipient_name = c.String(),
                        state = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        fileName = c.String(),
                        fileExtenstion = c.String(),
                        ContentType = c.String(),
                        Type = c.String(),
                        OriginalName = c.String(),
                        Size = c.String(),
                        Createdate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Isdeleted = c.Boolean(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MachineSizejns",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        SizeID = c.String(),
                        MachineID = c.String(),
                        Isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MachineBoostjns",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        BoostID = c.String(),
                        MachineID = c.String(),
                        Positions = c.Int(nullable: false),
                        Isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Boosts", "Machine_Id", "dbo.Machines");
            DropIndex("dbo.Boosts", new[] { "Machine_Id" });
            DropColumn("dbo.Boosts", "Machine_Id");
        }
    }
}
