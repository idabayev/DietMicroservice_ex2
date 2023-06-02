using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class Mapper
    {
        public DishDto ConvertToDishDto(IDish dish)
        {
            if (dish == null) return null;

            var dishDto = new DishDto();

            dishDto.name = dish.Name;
            dishDto.ID = dish.ID;
            dishDto.cal = dish.Cal;
            dishDto.size = dish.Size;
            dishDto.sodium = dish.Sodium;
            dishDto.sugar = dish.Sugar;

            return dishDto;
        }

        public MealDto ConvertToMealDto(IMeal meal)
        {
            if (meal == null) return null;

            var mealDto = new MealDto();

            mealDto.name = meal.Name;
            mealDto.ID = meal.ID;
            mealDto.appetizer = meal.Appetizer;
            mealDto.main = meal.Main;
            mealDto.dessert = meal.Dessert;
            mealDto.cal = meal.Cal;
            mealDto.sodium = meal.Sodium;
            mealDto.sugar = meal.Sugar;

            return mealDto;
        }
    }
}
