using System;
using System.Collections.Generic;

namespace Ex1_ApiForMeals
{
    public interface IDishesBusinessComponent
    {
        //SortedDictionary<int, DishDto> GetAllDishes();
        List<DishDto> GetAllDishes();
        DishDto GetDishById(int id);
        DishDto GetDishByName(string name);
        
        //List<int> AddDishToDb(string name); // for multiple dishes
        int AddDishToDb(string name); // for merged dishes
        int DeleteDishById(int id);
        int DeleteDishByName(string name);
    }
}
