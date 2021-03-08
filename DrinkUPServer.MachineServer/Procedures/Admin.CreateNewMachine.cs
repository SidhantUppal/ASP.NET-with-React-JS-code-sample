using DrinkUPServer.MachineServer.Defaults;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Admin
    {
        internal static async void CreateNewMachine ()
        {
            Machine machine = new Machine
            {
                Id = Guid.NewGuid().ToString(),

                InitializationToken = Guid.NewGuid().ToString(),
            };

            IEnumerable<Size> Sizes = (

                from size in NewMachine.Sizes

                select new Size
                {
                    Id = Guid.NewGuid().ToString(),

                    Machine = machine,
                    Enabled = size.Enabled,

                    Target = size.Target,
                    Title = size.Title,
                    Capacity = size.Capacity,
                    Price = size.Price,
                    Image = size.Image,
                }

                );

            IEnumerable<Boost> Boosts = (

                from boost in NewMachine.Boosts

                select new Boost
                {
                    Id = Guid.NewGuid().ToString(),

                    Machine = machine,
                    Enabled = boost.Enabled,

                    Target = boost.Target,
                    Percentage = boost.Percentage,
                    Duration = boost.Duration,

                    Title = boost.Title,
                    SubTitle = boost.SubTitle,
                    Price = boost.Price,
                    Image = boost.Image,
                    Ingredients = boost.Ingredients,
                    Details = boost.Details,
                }

                );

            await Pool.Database.AddMachine( machine );

            foreach ( Size size in Sizes )
            {
                await Pool.Database.AddSize( size );
            }

            foreach ( Boost boost in Boosts )
            {
                await Pool.Database.AddBoost( boost );
            }
        }
    }
}
