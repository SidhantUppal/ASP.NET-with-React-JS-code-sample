namespace DrinkUPServer.Database.Migrations
{
    using DrinkUPServer.Structures.Constructs;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DrinkUPServer.Database.DataServer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DrinkUPServer.Database.DataServer context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            Machine one = new Machine
            {
                Id = "mach-one",

                Name = "The First Machine",
                Location = "Earth",
            };

            context.Machines.AddOrUpdate( one );


            context.Sizes.AddOrUpdate( new Size
            {
                Id = "mach-one-size-small",

                Machine = one,
                Target = 0,
                Enabled = true,

                Title = "Small",
                Capacity = 16,
                Image = "SizeSmall",
                Price = 101,
            } );

            context.Sizes.AddOrUpdate( new Size
            {
                Id = "mach-one-size-medium",

                Machine = one,
                Target = 1,
                Enabled = true,

                Title = "Medium",
                Capacity = 26,
                Image = "SizeMedium",
                Price = 102,
            } );

            context.Sizes.AddOrUpdate( new Size
            {
                Id = "mach-one-size-large",

                Machine = one,
                Target = 2,
                Enabled = true,

                Title = "Large",
                Capacity = 40,
                Image = "SizeLarge",
                Price = 103,
            } );


            context.Boosts.AddOrUpdate( new Boost
            {
                Id = "mach-one-boost-electrolyte",

                Machine = one,
                Target = 0,
                Enabled = true,
                Duration = 1000,
                Percentage = 5,

                Title = "Electrolyte",
                SubTitle = "Strawberry Peach",
                Image = "BoostElectrolyte",
                Price = 76,
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Ingredients = "Strawberries and Peaches",
            } );

            context.Boosts.AddOrUpdate( new Boost
            {
                Id = "mach-one-boost-protein",

                Machine = one,
                Target = 1,
                Enabled = true,
                Duration = 1000,
                Percentage = 5,

                Title = "Protein",
                SubTitle = "Watermelon",
                Image = "BoostProtein",
                Price = 77,
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Ingredients = "Collagen and watermelon",
            } );

            context.Boosts.AddOrUpdate( new Boost
            {
                Id = "mach-one-boost-energy",

                Machine = one,
                Target = 2,
                Enabled = true,
                Duration = 1000,
                Percentage = 5,

                Title = "Energy",
                SubTitle = "Cranberry Grape",
                Image = "BoostEnergy",
                Price = 78,
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Ingredients = "Cranberries and grapes",
            } );

            context.Boosts.AddOrUpdate( new Boost
            {
                Id = "mach-one-boost-immunity",

                Machine = one,
                Target = 3,
                Enabled = true,
                Duration = 1000,
                Percentage = 5,

                Title = "Immunity",
                SubTitle = "Black Cherry",
                Image = "BoostImmunity",
                Price = 79,
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Ingredients = "Black cherries maybe",
            } );

        }
    }
}
