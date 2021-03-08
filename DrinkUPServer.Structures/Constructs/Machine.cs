using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DrinkUPServer.Structures.Constructs
{
    public class Machine
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string InitializationToken { get; set; }
    }
}
