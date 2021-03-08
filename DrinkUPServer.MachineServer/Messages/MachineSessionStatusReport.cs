using DrinkUPServer.MachineServer.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.ReportMachineSession )]
    public class MachineSessionStatusReport : ClientSentMessage
    {
    }
}
