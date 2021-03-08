using DrinkUPServer.MachineServer.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Functions
    {
        internal static void TriggerDispensation ( string For, string Size, string Boost )
        {
            Pool.Connection.Send( new WebSessionStatusUpdate
            {
                For = For,

                Active = true,
                Phase = WebSessionStatusUpdate.Phases.Dispense,

                Size = Size,
                Boost = Boost,
            } );
        }
    }
}
