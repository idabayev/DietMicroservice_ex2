using System;
using System.Collections.Generic;

namespace Ex1_ApiForMeals
{
    public interface IMealsBusinessComponent
    {
        int AddMealToDb(MealRoutingObject meal);
        //SortedDictionary<int, MealDto> GetAllMeals();
        List<MealDto> GetAllMeals();
        MealDto GetMealById(int id);
        MealDto GetMealByName(string name);
        int DeleteMealById(int id);
        int DeleteMealByName(string name);
        int UpdateMealById(int id,MealRoutingObject meal);
    }
}
