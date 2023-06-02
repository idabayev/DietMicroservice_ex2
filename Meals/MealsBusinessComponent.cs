using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ex1_ApiForMeals
{
    public class MealsBusinessComponent : IMealsBusinessComponent
    {
        private readonly IDbService _dbService;
        private readonly IDishesBusinessComponent _dishesBusinessComponent;
        private readonly Mapper _mapper;
        public MealsBusinessComponent()
        {
            _dbService = new DbService();
            _dishesBusinessComponent = new DishesBusinessComponent();
            _mapper = new Mapper();
        }

        public SortedDictionary<int, MealDto> GetAllMeals()
        {
            var meals = _dbService.GetAllMealsFromDb();
            var mealsDtoDictionary = new SortedDictionary<int, MealDto>();
            foreach (var keyValuePair in meals)
            {
                mealsDtoDictionary.Add(keyValuePair.Key, _mapper.ConvertToMealDto(keyValuePair.Value));
            }
            return mealsDtoDictionary;
        }

        public int AddMealToDb(MealRoutingObject mealObject)
        {
            if (_dbService.IsMealExist(mealObject.Name)) return (int)ApiStatusCode.DishOrMealAlreadyExist;

            var meal = new Meal(mealObject.Name);
            
            meal.Appetizer = mealObject.Appetizer;
            meal.Main = mealObject.Main;
            meal.Dessert = mealObject.Dessert;

            var mealsDishesIds = new List<int>() { meal.Appetizer.Value, meal.Main.Value, meal.Dessert.Value };

            foreach (var dishId in mealsDishesIds)
            {
                var dish = _dishesBusinessComponent.GetDishById(dishId);
                if (dish == null) return (int)ApiStatusCode.MealsDishNotExist;

                meal.Cal += dish.cal;
                meal.Sodium += dish.sodium;
                meal.Sugar += dish.sugar;
            }

            meal.ID = GenerateMealId();
            _dbService.SaveMeal(meal);
            return meal.ID;
        }



        public int UpdateMealById(int id, MealRoutingObject mealObject)
        {
            var meal = new Meal(mealObject.Name);

            meal.Appetizer = mealObject.Appetizer;
            meal.Main = mealObject.Main;
            meal.Dessert = mealObject.Dessert;

            var mealsDishesIds = new List<int>() { meal.Appetizer.Value, meal.Main.Value, meal.Dessert.Value };

            foreach (var dishId in mealsDishesIds)
            {
                var dish = _dishesBusinessComponent.GetDishById(dishId);
                if (dish == null) return (int)ApiStatusCode.MealsDishNotExist;

                meal.Cal += dish.cal;
                meal.Sodium += dish.sodium;
                meal.Sugar += dish.sugar;
            }

            meal.ID = id;
            return _dbService.UpdateMealById(id, meal);
        }

        private int GenerateMealId()
        {
            return _dbService.GetAvailableMealId();
        }

        public MealDto GetMealById(int id)
        {
            var meal = _dbService.GetMealById(id);
            return _mapper.ConvertToMealDto(meal);
        }

        public MealDto GetMealByName(string name)
        {
            var meal = _dbService.GetMealByName(name);
            return _mapper.ConvertToMealDto(meal);
        }

        public int DeleteMealById(int id)
        {
            return _dbService.DeleteMealById(id);
        }

        public int DeleteMealByName(string name)
        {
            var meal = GetMealByName(name);
            if (meal == null) return (int)ApiStatusCode.DishOrMealNotFound;

            return DeleteMealById(meal.ID);
        }
    }
}
