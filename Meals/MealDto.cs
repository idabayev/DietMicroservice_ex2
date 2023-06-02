using System;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class MealDto
    {
        public string name { get; set; }

        [JsonPropertyName("ID")]
        public int ID { get; set; }
        public int? main { get; set; }
        public int? appetizer { get; set; }
        public int? dessert { get; set; }
        public float cal { get; set; }
        public float sodium { get; set; }
        public float sugar { get; set; }
        
    }
}
