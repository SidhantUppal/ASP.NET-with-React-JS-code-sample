using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.DeliverySizes )]
    public class SizesDelivery : ServerSentMessage
    {
        public List<Size> Sizes { get; set; }
    }
}
