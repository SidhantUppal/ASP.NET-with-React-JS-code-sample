using DrinkUPServer.MachineServer.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.DeliveryQuery )]
    public class QueryDelivery : ServerSentMessage
    {
        public string Query { get; set; }
        public string Code { get; set; }
    }
}
