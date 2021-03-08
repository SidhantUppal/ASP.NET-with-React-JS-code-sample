using DrinkUPServer.Structures.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Request
    {
        internal static int Counter = 0;
        internal static ReadinessResponse Readiness ( ReadinessReference readinessReference )
        {
            if ( readinessReference.TransactionId == "mach-one-communication-guid" )
            {
                if ( readinessReference.TransactionSecret == "secret-key" )
                {
                    if ( Counter++ > 3 )
                    {
                        return new ReadinessResponse
                        {
                            Status = ReadinessResponse.Statuses.Ready,
                        };
                    }
                    else
                    {
                        return new ReadinessResponse
                        {
                            Status = ReadinessResponse.Statuses.StandBy,
                        };
                    }
                }
            }
            else
            {
                if ( readinessReference.TransactionId == "mach-one-communication-guid" )
                {
                    if ( readinessReference.TransactionSecret == "secret-key" )
                    {
                        if ( Counter++ > 3 )
                        {
                            return new ReadinessResponse
                            {
                                Status = ReadinessResponse.Statuses.Ready,
                            };
                        }
                        else
                        {
                            return new ReadinessResponse
                            {
                                Status = ReadinessResponse.Statuses.StandBy,
                            };
                        }
                    }
                }
            }
            return new ReadinessResponse
            {
                Status = ReadinessResponse.Statuses.Fault,
            };
        }
    }
}
