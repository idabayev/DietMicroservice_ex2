using System;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class Dish : IDish
    {
        [JsonIgnore]
        public string _id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        public int ID { get; set; }

        [JsonPropertyName("calories")]
        public float Cal { get; set; }

        [JsonPropertyName("serving_size_g")] 
        public float Size { get; set; }

        [JsonPropertyName("sodium_mg")]
        public float Sodium { get; set; }

        [JsonPropertyName("sugar_g")]
        public float Sugar { get; set; }
    }
}
