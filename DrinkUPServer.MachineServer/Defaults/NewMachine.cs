using DrinkUPServer.MachineServer.Defaults.Helpers;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Defaults
{
    internal static class NewMachine
    {
        public static List<SizeInitialization> Sizes = new List<SizeInitialization>
        {
            new SizeInitialization{
                Title = "Small",
                Capacity = 16,
                Price = 75,
                Image = "SizeSmall",

                Enabled = true,
                Target = 1,
            },
            new SizeInitialization{
                Title = "Medium",
                Capacity = 26,
                Price = 125,
                Image = "SizeMedium",

                Enabled = true,
                Target = 2,
            },
            new SizeInitialization{
                Title = "Large",
                Capacity = 40,
                Price = 175,
                Image = "SizeLarge",

                Enabled = true,
                Target = 3,
            },
        };

        public static List<BoostInitialization> Boosts = new List<BoostInitialization>
        {
            new BoostInitialization
            {
                Title = "Electrolyte",
                SubTitle = "Strawberry Peach",
                Price = 75,
                Image = "BoostElectrolyte",
                Ingredients = "Strawberries and peaches presumably",
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

                Enabled = true,
                Target = 1,
                Duration = 1000,
                Percentage = 5,
            },
            new BoostInitialization
            {
                Title = "Protein",
                SubTitle = "Watermelon",
                Price = 75,
                Image = "BoostProtein",
                Ingredients = "Watermelon flavouring and collagen",
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

                Enabled = true,
                Target = 1,
                Duration = 1000,
                Percentage = 5,
            },
            new BoostInitialization
            {
                Title = "Energy",
                SubTitle = "Cranberry Grape",
                Price = 75,
                Image = "BoostEnergy",
                Ingredients = "Cranberries and grapes presumably",
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

                Enabled = true,
                Target = 1,
                Duration = 1000,
                Percentage = 5,
            },
            new BoostInitialization
            {
                Title = "Immunity",
                SubTitle = "Black Cherry",
                Price = 75,
                Image = "BoostImmunity",
                Ingredients = "Black cherries presumably",
                Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",

                Enabled = true,
                Target = 1,
                Duration = 1000,
                Percentage = 5,
            },
        };
    }
}
