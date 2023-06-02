using System;
using System.Collections.Generic;

namespace Ex1_ApiForMeals
{
    public interface IDbService
    {
        SortedDictionary<int, IDish> GetAllDishesFromDb();
        SortedDictionary<int, IMeal> GetAllMealsFromDb();
        int GetAvailableDishId();
        void SaveDish(Dish dish);
        int DeleteDishById(int id);
        IDish GetDishById(int id);
        IDish GetDishByName(string name);
        bool IsDishExist(string name);
        bool IsMealExist(string name);
        int GetAvailableMealId();
        void SaveMeal(Meal meal);
        IMeal GetMealByName(string name);
        IMeal GetMealById(int id);
        int DeleteMealById(int id);
        int UpdateMealById(int id, Meal meal);
    }
}
