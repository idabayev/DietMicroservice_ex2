using System;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class DishDto
    {
        public string name { get; set; }

        [JsonPropertyName("ID")]
        public int ID { get; set; }
        public float cal { get; set; }
        public float size { get; set; }
        public float sodium { get; set; }
        public float sugar { get; set; }
    }
}
