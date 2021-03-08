using DrinkUPServer.Structures.API;
using DrinkUPServer.Structures.Constructs;
using DrinkUPServer.Structures.SessionManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Request
    {
        internal static DispensationResponse Dispense ( DispensationReference dispensationReference )
        {
            if ( dispensationReference.TransactionId == "mach-one-communication-guid" )
            {
                Counter = 0;
                Start = DateTime.UnixEpoch;

                return new DispensationResponse
                {
                    Status = DispensationResponse.Statuses.Done,
                };
            }
            else
            {
                if ( Pool.GetSessionFromTransactionId( dispensationReference.TransactionId ) is WebSession session )
                {
                    Functions.TriggerDispensation(
                        For: session.Machine,
                        Size: dispensationReference.Size,
                        Boost: dispensationReference.Boost
                        );

                    return new DispensationResponse
                    {
                        Status = DispensationResponse.Statuses.Done,
                    };
                }


                //Machine machine = Pool.Access.GetMachine( dispensationReference.Machine ).Result;

                //if ( Pool.MachineConnected( dispensationReference.Machine ) )
                //{
                //    if ( Pool.GetSessionFromMachineId( dispensationReference.Machine ) is WebSession webSession )
                //    {
                //        if ( webSession.Transaction == dispensationReference.TransactionId )
                //        {
                //            Functions.TriggerDispensation(
                //                For: machine.Id,
                //                Size: dispensationReference.Size,
                //                Boost: dispensationReference.Boost
                //                );

                //            return new DispensationResponse
                //            {
                //                Status = DispensationResponse.Statuses.Done,
                //            };
                //        }
                //    }
                //}
            }
            return new DispensationResponse
            {
                Status = DispensationResponse.Statuses.Failure,
            };
        }
    }
}
