using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DrinkUPServer.Structures.Constructs
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
