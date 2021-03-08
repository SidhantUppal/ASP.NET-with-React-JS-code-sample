using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    [Message( "system-announce" )]
    internal class RequestAnnouncement : ServerSentMessage
    {
    }
}
