using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.DeliveryBoosts )]
    public class BoostsDelivery : ServerSentMessage
    {
        public List<Boost> Boosts { get; set; }
    }
}
