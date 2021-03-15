using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.Structures.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUPServer.Web.Controllers
{
    [ApiController]
    [Route( "endpoint/[controller]" )]
    public class RequestController : ControllerBase
    {
        [HttpPost]
        [Route( "connect" )]
        public ConnectionResponse Connection ( [FromBody] ConnectionReference connectionReference )
        {
            Utility.LogFile("Connection", "Request Controller");
            try
            {
                return MachineServer.Program.Connect(connectionReference);
            }
            catch(Exception ex)
            {
                Utility.LogFile(ex.Message, "Request Controller");
                return new ConnectionResponse
                {
                    Status = ConnectionResponse.Statuses.Failure
                };
            }
        }

        [HttpPost]
        [Route( "availability" )]
        public AvailabilityResponse Availability ( [FromBody] AvailabilityReference availabilityReference )
        {
            Utility.LogFile("Availability", "Request Controller");
            return MachineServer.Program.Avail( availabilityReference );
        }

        [HttpPost]
        [Route( "readiness" )]
        public ReadinessResponse Readiness ( [FromBody] ReadinessReference readinessReference )
        {
            Utility.LogFile("Readiness", "Request Controller");
            return MachineServer.Program.Ready( readinessReference );
        }

        [HttpPost]
        [Route( "dispense" )]
        public DispensationResponse Dispensation ( [FromBody] DispensationReference dispensationReference )
        {
            Utility.LogFile("Dispensation", "Request Controller");
            return MachineServer.Program.Dispense( dispensationReference );
        }
    }
}
