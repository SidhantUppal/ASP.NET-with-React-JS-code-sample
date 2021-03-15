using DrinkUPServer.Structures.Constructs;
using System;
using System.Data.Entity;

namespace DrinkUPServer.Database
{
    internal class DataServer : DbContext
    {
        public DataServer ()
            // Here's where to set the Database query string.
            /// : base( @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DrUPDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" ) { }
            : base(@"Server=tcp:drinkupserver.database.windows.net,1433;Initial Catalog=DrUPBD;Persist Security Info=False;User ID=drinkup;Password=Talentelgia123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;") { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Boost> Boosts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
