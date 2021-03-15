using DrinkUPServer.MachineServer.Communication;
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
            Utility.LogFile("RequestStructures", "Functions");
            Pool.Connection.RegisterMessageListener<StructuresRequest>( RequestStructuresHandler );
        }

        private static async void RequestStructuresHandler ( StructuresRequest message )
        {
            var ipDetails = await Pool.Database.GetAzureIPDetails();

            Machine machine = await Pool.Database.GetMachine( message.From );
            Utility.LogFile(machine.Name, "Functions");
            SizesDelivery sizeDelivery = new SizesDelivery
            {
                For = message.From,
                Sizes = await Pool.Database.GetSizes( machine ),
            };

            Pool.Connection.Send( sizeDelivery );
            Utility.LogFile("Send size", "Functions");
            BoostsDelivery boostDelivery = new BoostsDelivery
            {
                For = message.From,
                Boosts = await Pool.Database.GetBoosts( machine ),
            };

            Pool.Connection.Send( boostDelivery );
            Utility.LogFile("Send boost", "Functions");
        }
    }
}
