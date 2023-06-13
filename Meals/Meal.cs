using System;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class Meal : IMeal
    {
        [JsonIgnore]
        public string _id { get; set; }
        public Meal(string name)
        {
            Name = name;
            Cal = 0;
            Sodium = 0;
            Sugar = 0;
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public int? Main { get; set; }
        public int? Appetizer { get; set; }
        public int? Dessert { get; set; }
        public float Cal { get; set; }
        public float Sodium { get; set; }
        public float Sugar { get; set; }
        
    }
}
