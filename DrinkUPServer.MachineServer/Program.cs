using DrinkUPServer.MachineServer.Communication;
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
            Utility.LogFile("Run", "Program");
        }

        public static ConnectionResponse Connect ( ConnectionReference connectionReference )
        {
            Utility.LogFile("Connect", "Program");
            return Request.Connect( connectionReference );
        }

        public static AvailabilityResponse Avail ( AvailabilityReference availabilityReference )
        {
            Utility.LogFile("Avail", "Program");
            return Request.Availability( availabilityReference );
        }

        public static ReadinessResponse Ready ( ReadinessReference readinessReference )
        {
            Utility.LogFile("Ready", "Program");
            return Request.Readiness( readinessReference );
        }

        public static DispensationResponse Dispense ( DispensationReference dispensationReference )
        {
            Utility.LogFile("Dispense", "Program");
            return Request.Dispense( dispensationReference );
        }
    }
}
