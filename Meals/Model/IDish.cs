using System;

namespace Ex1_ApiForMeals
{
    public interface IDish
    {
        public string Name { get; set; }
        public string _id { get; set; }
        public int ID { get; set; }
        public float Cal { get; set; }
        public float Size { get; set; }
        public float Sodium { get; set; }
        public float Sugar { get; set; }
    }
}
