using DrinkUPServer.Structures.API;
using DrinkUPServer.Structures.Constructs;
using DrinkUPServer.Structures.SessionManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DrinkUPServer.MachineServer.Communication;

namespace DrinkUPServer.MachineServer.Procedures
{
    internal static partial class Request
    {
        internal static ConnectionResponse Connect ( ConnectionReference connectionReference )
        {
            Utility.LogFile(connectionReference.Query, "Request Connet");
            if ( connectionReference.Query == "mach-one-query" )
            {
                try
                {
                    Machine machine = new Machine
                    {
                        Id = "mach-one",
                    };

                    if ( machine != null )
                    {
                        Start = DateTime.Now;

                        List<IApiSize> sizes = Pool.Database.GetSizes<IApiSize>( machine ).Result;
                        List<IApiBoost> boosts = Pool.Database.GetBoosts<IApiBoost>( machine ).Result;

                        return new ConnectionResponse
                        {
                            Status = ConnectionResponse.Statuses.Success,
                            TransactionId = "mach-one-communication-guid",
                            Sizes = sizes,
                            Boosts = boosts,
                        };
                    }
                }
                catch(Exception ex) { Utility.LogFile(ex.Message, "Request"); }
            }
            //else
            //{
            //    Machine machine = Access.GetMachineByQueryId( connectionReference.Query ).Result;
            //    if ( machine != null )
            //    {
            //        if ( Pool.MachineConnected( machine.Id ) )
            //        {
            //            if ( !Pool.MachineInUse( machine.Id ) )
            //            {
            //                WebSession webSession = new WebSession( machine.Id );

            //                if ( Pool.SetSession( webSession ) )
            //                {
            //                    return new ConnectionResponse
            //                    {
            //                        Status = ConnectionResponse.Success,
            //                        Machine = webSession.Machine,
            //                        Transaction_Id = webSession.Transaction,
            //                        Sizes = Access.GetSizes( machine ).Result,
            //                        Boosts = Access.GetBoosts( machine ).Result
            //                    };
            //                }
            //            }
            //        }
            //    }
            //}
            else
            {
                Utility.LogFile(connectionReference.Query, "Request Conncet else");
                if ( Pool.HasMachineQuery( connectionReference.Query ) )
                {
                    string machine = Pool.GetMachineFromQuery( connectionReference.Query );

                    if ( Pool.MachineConnected( machine ) )
                    {
                        if ( !Pool.MachineInUse( machine ) )
                        {
                            WebSession webSession = new WebSession( machine );

                            if ( Pool.SetSession( webSession ) )
                            {
                                List<IApiSize> sizes = Pool.Database.GetSizes<IApiSize>( machine ).Result;
                                List<IApiBoost> boosts = Pool.Database.GetBoosts<IApiBoost>( machine ).Result;

                                return new ConnectionResponse
                                {
                                    Status = ConnectionResponse.Statuses.Success,
                                    TransactionId = webSession.Transaction,
                                    Sizes = sizes,
                                    Boosts = boosts,
                                };
                            }
                        }
                    }
                }
            }
            return new ConnectionResponse
            {
                Status = ConnectionResponse.Statuses.Failure,
            };
        }
    }
}
