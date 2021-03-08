using DrinkUPServer.MachineServer.Procedures;
using DrinkUPServer.Structures.API;
using System;
using System.Diagnostics;

namespace DrinkUPServer.MachineServer
{
    public class Program
    {
        public static void Run ()
        {
            Functions.Initialize();
        }

        public static ConnectionResponse Connect ( ConnectionReference connectionReference )
        {
            return Request.Connect( connectionReference );
        }

        public static AvailabilityResponse Avail ( AvailabilityReference availabilityReference )
        {
            return Request.Availability( availabilityReference );
        }

        public static ReadinessResponse Ready ( ReadinessReference readinessReference )
        {
            return Request.Readiness( readinessReference );
        }

        public static DispensationResponse Dispense ( DispensationReference dispensationReference )
        {
            return Request.Dispense( dispensationReference );
        }
    }
}
