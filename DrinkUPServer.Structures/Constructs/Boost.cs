using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DrinkUPServer.Structures.Constructs
{
    public interface IBaseBoost { }

    // Portion of Boost available to the Web API
    public interface IApiBoost : IBaseBoost
    {
        string Id { get; set; }
        string Title { get; set; }
        string SubTitle { get; set; }
        string Image { get; set; }
        int Price { get; set; }
        string Ingredients { get; set; }
        string Details { get; set; }
    }

    // Server-relevant remaining portion
    public interface IBoost : IApiBoost
    {
        Machine Machine { get; set; }
        int Target { get; set; }
        bool Enabled { get; set; }
        int Percentage { get; set; }
        int Duration { get; set; }
    }

    public class Boost: Base, IBoost
    {
        [Key]
        public string Id { get; set; }

        [JsonIgnore]
        public Machine Machine { get; set; }

        [JsonIgnore]
        public int Target { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public int Percentage { get; set; }

        [JsonIgnore]
        public int Duration { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }

        public string Ingredients { get; set; }
        public string Details { get; set; }
        public string MediaID { get; set; }
        public bool Isdeleted { get; set; }
        public string Comment { get; set; }
    }
}
