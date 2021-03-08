using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DrinkUPServer.Structures.Constructs
{
    public class Transaction : Base
    {
        [Key]
        public string Id { get; set; }

        public Machine Machine { get; set; }

        public Size Size { get; set; }
        public Boost Boost { get; set; }

        public double Total { get; set; }
    }
}
