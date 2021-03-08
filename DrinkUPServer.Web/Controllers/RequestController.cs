using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return MachineServer.Program.Connect( connectionReference );
        }

        [HttpPost]
        [Route( "availability" )]
        public AvailabilityResponse Availability ( [FromBody] AvailabilityReference availabilityReference )
        {
            return MachineServer.Program.Avail( availabilityReference );
        }

        [HttpPost]
        [Route( "readiness" )]
        public ReadinessResponse Readiness ( [FromBody] ReadinessReference readinessReference )
        {
            return MachineServer.Program.Ready( readinessReference );
        }

        [HttpPost]
        [Route( "dispense" )]
        public DispensationResponse Dispensation ( [FromBody] DispensationReference dispensationReference )
        {
            return MachineServer.Program.Dispense( dispensationReference );
        }
    }
}
