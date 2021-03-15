﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkUPServer.MachineServer.Communication
{
    public static class Utility
    {
        public static void LogFile(string sExceptionName, string sEventName)
        {
            StreamWriter log;
            if (!File.Exists("logfile.txt"))
            {
                log = new StreamWriter("logfile.txt");
            }
            else
            {
                log = File.AppendText("logfile.txt");
            }
            // Write to the file:
            log.WriteLine("Data Time:" + DateTime.Now);
            log.WriteLine("Exception Name:" + sExceptionName);
            log.WriteLine("Event Name:" + sEventName);
            log.WriteLine("-------------------------------------------------");
            log.WriteLine("                                                 ");
            // Close the stream:
            log.Close();
        }
    }
}
