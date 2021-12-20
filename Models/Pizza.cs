using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostgresConnect.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Vegitarian { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Size PizzaSize { get; set; }
        public enum Size
        { 
            small,
            medium,
            large
        }
    }
}
