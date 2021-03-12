using DrinkUPServer.MachineServer.Messages;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Functions
    {
        internal static void RequestStructures ()
        {
            Pool.Connection.RegisterMessageListener<StructuresRequest>( RequestStructuresHandler );
        }

        private static async void RequestStructuresHandler ( StructuresRequest message )
        {
            var ipDetails = await Pool.Database.GetAzureIPDetails();

            Machine machine = await Pool.Database.GetMachine( message.From );

            SizesDelivery sizeDelivery = new SizesDelivery
            {
                For = message.From,
                Sizes = await Pool.Database.GetSizes( machine ),
            };

            Pool.Connection.Send( sizeDelivery );

            BoostsDelivery boostDelivery = new BoostsDelivery
            {
                For = message.From,
                Boosts = await Pool.Database.GetBoosts( machine ),
            };

            Pool.Connection.Send( boostDelivery );
        }
    }
}
