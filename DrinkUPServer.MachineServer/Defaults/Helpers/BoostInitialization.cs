using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Defaults.Helpers
{
    internal class BoostInitialization
    {
        public int Target { get; set; }
        public bool Enabled { get; set; }
        
        public int Duration { get; set; }
        public int Percentage { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Ingredients { get; set; }
        public string Details { get; set; }
    }
}
