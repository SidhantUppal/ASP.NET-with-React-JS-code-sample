using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.MachineServer.Messages;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Functions
    {
        static Thread QueryDeliveringThread;

        internal static void QueryDelivering ()
        {
            QueryDeliveringThread = new Thread( QueryDeliveringRunner );
            QueryDeliveringThread.Start();
        }

        private static void QueryDeliveringRunner ()
        {
            while ( true )
            {
                Thread.Sleep( QueryDeliveringInterval );

                Pool.GetMachinesList().ForEach( ( For ) =>
                {
                    string query = Guid.NewGuid().ToString();

                    Pool.AddMachineQuery( For, query );
                    Pool.ReduceMachineQueries( For, QueryPerMachineMaximum );

                    QueryDelivery queryDelivery = new QueryDelivery
                    {
                        For = For,
                        Query = query
                    };

                    Pool.Connection.Send( queryDelivery );
                } );
            }
        }
    }
}
