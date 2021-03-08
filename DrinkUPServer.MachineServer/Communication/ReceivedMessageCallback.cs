using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication
{
    public delegate void ReceivedMessageCallback<T> ( T message ) where T : ClientSentMessage;
}
