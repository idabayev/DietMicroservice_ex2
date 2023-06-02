using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Ex1_ApiForMeals
{
    public class MealRoutingObject
    {
        //[Required]
        public string Name { get; set; }

        //[Required]
        public int? Main { get; set; }

        //[Required]
        public int? Appetizer { get; set; }

        //[Required]
        public int? Dessert { get; set; }
    }
}
