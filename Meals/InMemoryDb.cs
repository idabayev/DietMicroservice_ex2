using System;
using System.Collections.Generic;

namespace Ex1_ApiForMeals
{
    public class InMemoryDb
    {
        private static SortedDictionary<int, IDish> _dishesStorage = new SortedDictionary<int, IDish>();
        private static SortedDictionary<int, IMeal> _mealsStorage = new SortedDictionary<int, IMeal>();

        public static int GetDishesStorageSize()
        {
            return _dishesStorage.Keys.Count;
        }

        public static int DeleteDish(int id)
        {
            if (_dishesStorage.ContainsKey(id))
            {
                _dishesStorage.Remove(id);
                return id;
            }

            return (int)ApiStatusCode.DishOrMealNotFound;
        }


        public static int DeleteMeal(int id)
        {
            if (_mealsStorage.ContainsKey(id))
            {
                _mealsStorage.Remove(id);
                return id;
            }

            return (int)ApiStatusCode.DishOrMealNotFound;
        }

        public static SortedDictionary<int, IDish> RetrieveDishes()
        {
            return _dishesStorage;
        }

        public static void SaveDish(Dish dish)
        {
            _dishesStorage.Add(dish.ID, dish);
        }

        public static void SaveMeal(Meal meal)
        {
            _mealsStorage.Add(meal.ID, meal);
        }

        public static SortedDictionary<int, IMeal> RetrieveMeals()
        {
            return _mealsStorage;
        }

        public static IDish RetrieveDishById(int id)
        {
            if (_dishesStorage.ContainsKey(id))
            {
                return _dishesStorage[id];
            }

            return null;
        }

        public static IDish RetrieveDishByName(string name)
        {
            foreach (var keyValuePair in _dishesStorage)
            {
                if(keyValuePair.Value.Name == name)
                {
                    return keyValuePair.Value;
                }
            }

            return null;
        }

        public static void UpdateDeletedDishInMealsStorage(int dishId)
        {
            foreach (var keyValuePair in _mealsStorage)
            {
                if(keyValuePair.Value.Appetizer == dishId)
                {
                    keyValuePair.Value.Appetizer = null;
                }

                if (keyValuePair.Value.Main == dishId)
                {
                    keyValuePair.Value.Main = null;
                }

                if (keyValuePair.Value.Dessert == dishId)
                {
                    keyValuePair.Value.Dessert = null;
                }
            }
        }

        public static int UpdateMealById(int id, Meal meal)
        {
            if (false == _mealsStorage.ContainsKey(id)) return (int)ApiStatusCode.DishOrMealNotFound;

            _mealsStorage[id] = meal;
            return id;
        }

        public static IMeal RetrieveMealById(int id)
        {
            if (_mealsStorage.ContainsKey(id))
            {
                return _mealsStorage[id];
            }

            return null;
        }

        public static IMeal RetrieveMealByName(string name)
        {
            foreach (var keyValuePair in _mealsStorage)
            {
                if (keyValuePair.Value.Name == name)
                {
                    return keyValuePair.Value;
                }
            }

            return null;
        }
    }
}
