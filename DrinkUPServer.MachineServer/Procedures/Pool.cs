using DrinkUPServer.Database;
using DrinkUPServer.MachineServer.Communication;
using DrinkUPServer.MachineServer.Messages;
using DrinkUPServer.Structures.SessionManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DrinkUPServer.MachineServer.Procedures
{
    public static class Pool
    {
        public static readonly Connection Connection = new Connection();
        public static readonly DatabaseAccess Database = new DatabaseAccess();

        private static readonly object MachineSessionsLock = new object();
        private static readonly Dictionary<string, Session> MachineSessions = new Dictionary<string, Session>();

        private static readonly object QueryToMachineLock = new object();
        private static readonly Dictionary<string, string> QueryToMachine = new Dictionary<string, string>();

        private static readonly object TransactionStoreLock = new object();
        private static readonly Dictionary<string, Session> TransactionStore = new Dictionary<string, Session>();

        private static readonly object MachineQueriesLock = new object();
        private static readonly Dictionary<string, Queue<string>> MachineQueries = new Dictionary<string, Queue<string>>();

        static Pool ()
        {
            Connection.MachineConnected += WhenMachineConnected;
            Connection.MachineDisconnected += WhenMachineDisconnected;
        }

        private static void WhenMachineConnected ( string clientName )
        {
            Utility.LogFile(clientName, "WhenMachineConnected");
            lock ( MachineSessionsLock )
            {
                MachineSessions.Add( clientName, null );
            }

            lock ( MachineQueriesLock )
            {
                MachineQueries.Add( clientName, new Queue<string>() );
            }
        }

        private static void WhenMachineDisconnected ( string clientName )
        {
            Utility.LogFile(clientName, "WhenMachineDisConnected");
            lock ( MachineSessionsLock )
            {
                MachineSessions.Remove( clientName );
            }

            ReduceMachineQueries( clientName );

            lock ( MachineQueriesLock )
            {
                MachineQueries.Remove( clientName );
            }
        }

        internal static List<string> GetMachinesList()
        {
            //Utility.LogFile("GetMachinesList", "WhenMachineConnected");
            IEnumerable<string> enumerable;
            lock (MachineSessionsLock)
            {
                enumerable = (from m in MachineSessions select m.Key);
            }
            if (enumerable.ToList().Count > 0)
                Utility.LogFile("GetMachinesList " + enumerable.ToList().Count, "WhenMachineConnected");
            return enumerable.ToList();
        }

        internal static void ReduceMachineQueries ( string clientName, int min = 0 )
        {
            min = min < 0 ? 0 : min;

            lock ( MachineQueriesLock )
            {
                lock ( QueryToMachineLock )
                {
                    while ( MachineQueries[ clientName ].Count > min )
                    {
                        QueryToMachine.Remove( MachineQueries[ clientName ].Dequeue() );
                    }
                }
            }
        }

        internal static void AddMachineQuery ( string clientName, string query )
        {
            Utility.LogFile(clientName+" "+ query, "AddMachineQuery");
            lock ( MachineQueries )
            {
                lock ( QueryToMachineLock )
                {
                    MachineQueries[ clientName ].Enqueue( query );
                    QueryToMachine.Add( query, clientName );
                }
            }
        }

        internal static bool HasMachineQuery ( string query )
        {
            Utility.LogFile(query, "HasMachineQuery");
            bool output = false;
            lock ( QueryToMachineLock )
            {
                output = QueryToMachine.ContainsKey( query );
            }
            return output;
        }

        internal static string GetMachineFromQuery ( string query )
        {
            Utility.LogFile(query, "GetMachineFromQuery");
            string machine = null;
            lock ( QueryToMachineLock )
            {
                machine = QueryToMachine[ query ];
            }
            return machine;
        }

        internal static bool MachineConnected ( string machine )
        {
            Utility.LogFile(machine, "MachineConnected");
            bool output = false;
            lock ( MachineSessionsLock )
            {
                output = MachineSessions.ContainsKey( machine );
            }
            return output;
        }

        internal static bool MachineInUse ( string machine )
        {
            Utility.LogFile(machine, "MachineInUse");
            bool output = false;
            lock ( MachineSessionsLock )
            {
                if ( !MachineSessions.ContainsKey( machine ) )
                {
                    output = false;
                }
                else
                {
                    output = null != MachineSessions[ machine ];
                }
            }
            return output;
        }

        internal static Session GetSessionFromMachineId ( string machine )
        {
            Utility.LogFile(machine, "GetSessionFromMachineId");
            Session session = null;

            lock ( MachineSessionsLock )
            {
                session = MachineSessions[ machine ];
            }

            return session;
        }

        internal static Session GetSessionFromTransactionId ( string transactionId )
        {
            Utility.LogFile(transactionId, "GetSessionFromTransactionId");
            Session session = null;

            lock ( TransactionStoreLock )
            {
                if ( TransactionStore.ContainsKey( transactionId ) )
                {
                    session = TransactionStore[ transactionId ];
                }
            }
            return session;
        }

        internal static bool SetSession ( Session session )
        {
            Utility.LogFile(session.Machine, "SetSession");
            if ( MachineInUse( session.Machine ) )
            {
                return false;
            }
            else
            {
                lock ( MachineSessionsLock )
                {
                    MachineSessions[ session.Machine ] = session;
                }

                lock ( TransactionStoreLock )
                {
                    TransactionStore[ session.Transaction ] = session;
                }

                Connection.Send( new WebSessionStatusUpdate
                {
                    For = session.Machine,
                    Active = true,
                    Phase = WebSessionStatusUpdate.Phases.Start
                } );

                return true;
            }
        }

        internal static bool ClearSession ( string machine )
        {
            Utility.LogFile(machine, "ClearSession");
            if ( MachineInUse( machine ) )
            {
                Session session = GetSessionFromMachineId( machine );

                lock ( MachineSessionsLock )
                {
                    TransactionStore.Remove( session.Transaction );
                }

                lock ( TransactionStoreLock )
                {
                    MachineSessions[ machine ] = null;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
