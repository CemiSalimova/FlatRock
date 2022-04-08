using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTaskFlatRock.Models
{
    public class Item
    {
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("rating")]
        public string Rating { get; set; }
    }
}
