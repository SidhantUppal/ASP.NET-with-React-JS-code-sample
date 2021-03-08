using DrinkUPServer.Database;
using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.MachineServer.Messages;
using DrinkUPServer.Structures.SessionManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Functions
    {
        private static readonly int QueryDeliveringInterval = 30000;
        private static readonly int QueryPerMachineMaximum = 3;

        internal static void Initialize ()
        {
            RequestStructures();
            QueryDelivering();
        }
    }
}
