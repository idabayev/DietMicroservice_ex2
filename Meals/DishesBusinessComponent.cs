using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ex1_ApiForMeals
{
    public class DishesBusinessComponent : IDishesBusinessComponent
    {
        private readonly IDbService _dbService;
        private readonly IProviderService _providerService;
        private readonly Mapper _mapper;
        public DishesBusinessComponent()
        {
            _dbService = new DbService();
            _mapper = new Mapper();
        }

        #region Code for returning multiple dishes in case there are multiple results from api ninjas

        //We didnt know which code to use, so we created both.

        //public List<int> AddDishToDb(string name)
        //{
        //    var savedIds = new List<int>();

        //    var providerService = new ProviderService();
        //    try
        //    {
        //        var dishes = providerService.GetDishInfoFromExternalApiAsync(name);
        //        foreach (var dish in dishes.Result)
        //        {
        //            //Check if the dish with provide name is already. In case there are more than one dish, it will check the dish name return from the api (lowercase), and return -2 (422) even if only one dish exist and the other not.
        //            if ((dishes.Result.Count > 1 && _dbService.IsDishExist(dish.Name)) || _dbService.IsDishExist(name)) return new List<int>() { (int)ApiStatusCode.DishOrMealAlreadyExist };

        //            var dishId = GenerateDishId();
        //            dish.Name = dishes.Result.Count > 1 ? dish.Name : name; // override returned name due to differenct in capital letters
        //            dish.ID = dishId;

        //            _dbService.SaveDish(dish);
        //            savedIds.Add(dishId);
        //        }

        //        if(savedIds.Count == 0) return new List<int>() { (int)ApiStatusCode.DishNotFountInNinjasApi };

        //        return savedIds;
        //    }
        //    catch (WebException)
        //    {
        //        return new List<int>() { (int)ApiStatusCode.ApiNinjasNotReachable };
        //    }
        //}

        #endregion

        #region Code for merged dishes
        public int AddDishToDb(string name)
        {
            if (_dbService.IsDishExist(name)) return (int)ApiStatusCode.DishOrMealAlreadyExist;

            var providerService = new ProviderService();
            try
            {
                var dishes = providerService.GetDishInfoFromExternalApiAsync(name);
                if (dishes.Result.Count == 0) return (int)ApiStatusCode.DishNotFountInNinjasApi;

                var dish = new Dish();
                var dishId = GenerateDishId();
                dish.Name = name; // override returned name due to differenct in capital letters
                dish.ID = dishId;
                dish._id = dishId.ToString();

                foreach (var apiDish in dishes.Result)
                {
                    dish.Cal += apiDish.Cal;
                    dish.Sodium += apiDish.Sodium;
                    dish.Sugar += apiDish.Sugar;
                    dish.Size += apiDish.Size;
                }

                _dbService.SaveDish(dish);

                return dishId;
            }
            catch (WebException)
            {
                return (int)ApiStatusCode.ApiNinjasNotReachable;
            }
        }
        #endregion



        private int GenerateDishId()
        {
            return _dbService.GetAvailableDishId();
        }

        public int DeleteDishById(int id)
        {
            return _dbService.DeleteDishById(id);
        }

        public int DeleteDishByName(string name)
        {
            var dish = GetDishByName(name);
            if (dish == null) return (int)ApiStatusCode.DishOrMealNotFound;

            //return _dbService.DeleteDishById(dish.ID);
            return DeleteDishById(dish.ID);
        }

        //public SortedDictionary<int, DishDto> GetAllDishes()
        //{
        //    var dishes = _dbService.GetAllDishesFromDb();
        //    var dishesDtoDictionary = new SortedDictionary<int, DishDto>();
        //    foreach (var keyValuePair in dishes)
        //    {
        //        dishesDtoDictionary.Add(keyValuePair.Key, _mapper.ConvertToDishDto(keyValuePair.Value));
        //    }
        //    return dishesDtoDictionary;
        //}

        public List<DishDto> GetAllDishes()
        {
            var dishes = _dbService.GetAllDishesFromDb();
            var dishesDtoList = new List<DishDto>();
            foreach (var dish in dishes)
            {
                dishesDtoList.Add(_mapper.ConvertToDishDto(dish));
            }
            return dishesDtoList;
        }

        public DishDto GetDishById(int id)
        {
            var dish = _dbService.GetDishById(id);
            return dish == null ? null :_mapper.ConvertToDishDto(dish);
        }
        
        public DishDto GetDishByName(string name)
        {
            var dish = _dbService.GetDishByName(name);
            return dish == null ? null : _mapper.ConvertToDishDto(dish);
        }
    }
}
