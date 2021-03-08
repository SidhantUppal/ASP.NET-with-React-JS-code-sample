using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.SessionManagement
{
    public abstract class Session
    {
        public string Machine { get; private set; }
        public string Transaction { get; private set; }

        public Session ( string machine )
        {
            Machine = machine;

            Transaction = Guid.NewGuid().ToString();
        }
    }
}
