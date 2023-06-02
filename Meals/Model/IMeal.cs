using System;

namespace Ex1_ApiForMeals
{
    public interface IMeal
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int? Appetizer { get; set; }
        public int? Main { get; set; }
        public int? Dessert { get; set; }
        public float Cal { get; set; }
        public float Sodium { get; set; }
        public float Sugar { get; set; }
    }
}
