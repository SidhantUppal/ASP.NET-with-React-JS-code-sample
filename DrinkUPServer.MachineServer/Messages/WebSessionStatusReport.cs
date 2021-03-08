using DrinkUPServer.MachineServer.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.ReportWebSession )]
    public class WebSessionStatusReport : ClientSentMessage
    {
    }
}
