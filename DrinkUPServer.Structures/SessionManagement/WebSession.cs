using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.SessionManagement
{
    public class WebSession : Session
    {
        const int ExpiryAllowance = 60;
        const int SessionDuration = 180;
        public DateTime Expiry { get; private set; }

        public bool Healthy
        {
            get
            {
                return DateTime.Now.AddSeconds( ExpiryAllowance ) > Expiry;
            }
        }

        public bool ExpiryImminent
        {
            get
            {
                return DateTime.Now.AddSeconds( ExpiryAllowance ) <= Expiry && DateTime.Now <= Expiry;
            }
        }

        public bool Expired
        {
            get
            {
                return DateTime.Now > Expiry;
            }
        }

        public WebSession ( string machine ) : base( machine )
        {
            Extend();
        }

        public void Extend ()
        {
            Expiry = DateTime.Now.AddSeconds( SessionDuration );
        }
    }
}
