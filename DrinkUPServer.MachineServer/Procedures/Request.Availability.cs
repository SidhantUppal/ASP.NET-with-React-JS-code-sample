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
        internal static AvailabilityResponse Availability ( AvailabilityReference availabilityReference )
        {
            if ( availabilityReference.Machine == "mach-one-fixed-id" )
            {
                if ( availabilityReference.TransactionId == "mach-one-communication-guid" )
                {
                    if ( Start.Add( TimeSpan.FromSeconds( 150 ) ) < DateTime.Now )
                    {
                        return new AvailabilityResponse
                        {
                            Status = AvailabilityResponse.Statuses.Connected,
                        };
                    }
                    else if ( Start.Add( TimeSpan.FromSeconds( 180 ) ) < DateTime.Now )
                    {
                        if ( availabilityReference.ContinuationKey == "mach-one-continuation" )
                        {
                            Counter = 0;
                            Start = DateTime.Now;
                            return new AvailabilityResponse
                            {
                                Status = AvailabilityResponse.Statuses.Connected,
                            };
                        }
                        else
                        {
                            return new AvailabilityResponse
                            {
                                Status = AvailabilityResponse.Statuses.Problem,
                            };
                        }
                    }
                }
            }
            else
            {
                if ( Pool.MachineConnected( availabilityReference.Machine ) )
                {
                    if ( Pool.GetSessionFromMachineId( availabilityReference.Machine ) is WebSession webSession )
                    {
                        if ( availabilityReference.TransactionId == webSession.Transaction )
                        {
                            if ( webSession.Healthy )
                            {
                                return new AvailabilityResponse
                                {
                                    Status = AvailabilityResponse.Statuses.Connected,
                                };
                            }
                            else if ( webSession.ExpiryImminent )
                            {
                                if ( availabilityReference.ContinuationKey != null )
                                {
                                    webSession.Extend();

                                    return new AvailabilityResponse
                                    {
                                        Status = AvailabilityResponse.Statuses.Connected,
                                    };
                                }
                                else
                                {
                                    return new AvailabilityResponse
                                    {
                                        Status = AvailabilityResponse.Statuses.Problem,
                                    };
                                }
                            }
                            else if ( webSession.Expired )
                            {
                                return new AvailabilityResponse
                                {
                                    Status = AvailabilityResponse.Statuses.Fault,
                                };
                            }
                        }
                    }
                }
            }

            Start = DateTime.UnixEpoch;
            return new AvailabilityResponse
            {
                Status = AvailabilityResponse.Statuses.Fault,
            };
        }
    }
}
