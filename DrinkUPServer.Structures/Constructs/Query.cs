using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DrinkUPServer.Structures.Constructs
{
    public class Query: Base
    {
        [Key]
        public string QueryId { get; set; }

        public string Machine { get; set; }

        public Nullable<DateTime> Expiry { get; set; }
    }
}
