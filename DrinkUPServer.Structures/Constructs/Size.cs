using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DrinkUPServer.Structures.Constructs
{
    public interface IBaseSize { }

    // Portion of Size available to the Web API
    public interface IApiSize : IBaseSize
    {
        string Id { get; set; }
        string Title { get; set; }
        string Image { get; set; }
        int Capacity { get; set; }
        int Price { get; set; }
    }

    // Server-relevant remaining portion
    public interface ISize : IApiSize
    {
        Machine Machine { get; set; }
        int Target { get; set; }
        bool Enabled { get; set; }
    }

    public class Size : Base, ISize
    {
        [Key]
        public string Id { get; set; }

        [JsonIgnore]
        public Machine Machine { get; set; }

        [JsonIgnore]
        public int Target { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }
        public int Capacity { get; set; }
        public int Price { get; set; }
    }
}
