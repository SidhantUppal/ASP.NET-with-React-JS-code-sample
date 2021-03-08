using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    [Message( "system-report" )]
    internal class ReportExistence : ClientSentMessage
    {
    }
}
