using System;
using System.Collections.Generic;

namespace Ex1_ApiForMeals
{
    public class DbService : IDbService
    {
        private static HashSet<int> dishesIds = new HashSet<int>();
        private static HashSet<int> mealsIds = new HashSet<int>();
  
        public int GetAvailableDishId()
        {
            var amountOfUsedIds = dishesIds.Count;
            var assignedId = amountOfUsedIds + 1;
            dishesIds.Add(assignedId);
            return assignedId;
        }

        public int GetAvailableMealId()
        {
            var amountOfUsedIds = mealsIds.Count;
            var assignedId = amountOfUsedIds + 1;
            mealsIds.Add(assignedId);
            return assignedId;
        }

        public SortedDictionary<int, IDish> GetAllDishesFromDb()
        {
            var dishes = InMemoryDb.RetrieveDishes();
            return dishes;
        }

        public SortedDictionary<int, IMeal> GetAllMealsFromDb()
        {
            var meals = InMemoryDb.RetrieveMeals();
            return meals;
        }

        public void SaveDish(Dish dish)
        {
            InMemoryDb.SaveDish(dish);
        }

        public void SaveMeal(Meal meal)
        {
            InMemoryDb.SaveMeal(meal);
        }

        public IDish GetDishById(int id)
        {
            return InMemoryDb.RetrieveDishById(id);
        }

        public IDish GetDishByName(string name)
        {
            return InMemoryDb.RetrieveDishByName(name);
        }


        public IMeal GetMealById(int id)
        {
            return InMemoryDb.RetrieveMealById(id);
        }
        public IMeal GetMealByName(string name)
        {
            return InMemoryDb.RetrieveMealByName(name);
        }

        public int DeleteDishById(int id)
        {
            InMemoryDb.UpdateDeletedDishInMealsStorage(id);
            return InMemoryDb.DeleteDish(id); ;
        }
        public int DeleteMealById(int id)
        {
            var deletedId = InMemoryDb.DeleteMeal(id);
            return deletedId;
        }

        public bool IsDishExist(string name)
        {
            return GetDishByName(name) != null;
        }

        public bool IsMealExist(string name)
        {
            return GetMealByName(name) != null;
        }

        public int UpdateMealById(int id, Meal meal)
        {
            return InMemoryDb.UpdateMealById(id, meal);
        }
    }
}
