using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Ex1_ApiForMeals
{
    public class DbService : IDbService
    {
        //private static HashSet<int> dishesIds = new HashSet<int>();
        //private static HashSet<int> mealsIds = new HashSet<int>();
        private readonly IMongoCollection<BsonDocument> _mealsCollection;
        private readonly IMongoCollection<BsonDocument> _dishesCollection;
        private readonly IMongoCollection<IdCounter> _dishesIdCounterCollection;
        private readonly IMongoCollection<IdCounter> _mealsIdCounterCollection;

        public DbService()
        {
            // Connection string for the MongoDB container
            string connectionString = "mongodb://localhost:27017";

            // Create a MongoDB client
            var client = new MongoClient(connectionString);

            // Access the database
            var database = client.GetDatabase("meals_dishes_diet_db");

            // Access a specific collection
            _mealsCollection = database.GetCollection<BsonDocument>("meals_collection");
            _dishesCollection = database.GetCollection<BsonDocument>("dishes_collection");
            _dishesIdCounterCollection = database.GetCollection<IdCounter>("dishes_id_counter_collection");
            _mealsIdCounterCollection = database.GetCollection<IdCounter>("meals_id_counter_collection");
        }

        //public int GetAvailableDishId()
        //{
        //    var amountOfUsedIds = dishesIds.Count;
        //    var assignedId = amountOfUsedIds + 1;
        //    dishesIds.Add(assignedId);
        //    return assignedId;
        //}

        public int GetAvailableDishId()
        {
            var counterCollection = _dishesIdCounterCollection;

            var counterDocument = counterCollection.FindOneAndUpdate(
                Builders<IdCounter>.Filter.Eq(c => c.Id, "ID"),
                Builders<IdCounter>.Update.Inc(c => c.Value, 1),
                new FindOneAndUpdateOptions<IdCounter>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                }
            );

            return counterDocument.Value;
        }

        //public int GetAvailableMealId()
        //{
        //    var amountOfUsedIds = mealsIds.Count;
        //    var assignedId = amountOfUsedIds + 1;
        //    mealsIds.Add(assignedId);
        //    return assignedId;
        //}

        public int GetAvailableMealId()
        {
            var counterCollection = _mealsIdCounterCollection;

            var counterDocument = counterCollection.FindOneAndUpdate(
                Builders<IdCounter>.Filter.Eq(c => c.Id, "ID"),
                Builders<IdCounter>.Update.Inc(c => c.Value, 1),
                new FindOneAndUpdateOptions<IdCounter>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                }
            );

            return counterDocument.Value;
        }

        public List<Dish> GetAllDishesFromDb()
        {
            //var dishes = InMemoryDb.RetrieveDishes();
            var dishesCollection = _dishesCollection.Find(new BsonDocument()).ToList();
            var dishes = BsonSerializer.Deserialize<List<Dish>>(dishesCollection.ToJson());
            return dishes;
        }
        
        //public SortedDictionary<int, Dish> GetAllDishesFromDb()
        //{
        //    //var dishes = InMemoryDb.RetrieveDishes();
        //    var dishesCollection = _dishesCollection.Find(new BsonDocument()).ToList();
        //    var deserilizedDishes = BsonSerializer.Deserialize<List<Dish>>(dishesCollection.ToJson()).ToDictionary(x => x.ID);
        //    var dishes = new SortedDictionary<int, Dish>(deserilizedDishes);
        //    return dishes;
        //}

        public List<Meal> GetAllMealsFromDb()
        {
            //var meals = InMemoryDb.RetrieveMeals();
            var mealsCollection = _mealsCollection.Find(new BsonDocument()).ToList();
            var meals = BsonSerializer.Deserialize<List<Meal>>(mealsCollection.ToJson());
            return meals;
        }

        //public SortedDictionary<int, Meal> GetAllMealsFromDb()
        //{
        //    //var meals = InMemoryDb.RetrieveMeals();
        //    var mealsCollection = _mealsCollection.Find(new BsonDocument()).ToList();
        //    var deserilizedMeals = BsonSerializer.Deserialize<List<Meal>>(mealsCollection.ToJson()).ToDictionary(x => x.ID);
        //    var meals = new SortedDictionary<int,Meal>(deserilizedMeals);
        //    return meals;
        //}

        public void SaveDish(Dish dish)
        {
            _dishesCollection.InsertOne(dish.ToBsonDocument());
            InMemoryDb.SaveDish(dish);
        }

        public void SaveMeal(Meal meal)
        {
            _mealsCollection.InsertOne(meal.ToBsonDocument());
            InMemoryDb.SaveMeal(meal);
        }

        public IDish GetDishById(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("ID", id);
            var result = _dishesCollection.Find(filter).ToList();
            var dishes = BsonSerializer.Deserialize<List<Dish>>(result.ToJson());
            return dishes.FirstOrDefault();
            //return InMemoryDb.RetrieveDishById(id);
        }

        public IDish GetDishByName(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
            var result = _dishesCollection.Find(filter).ToList();
            var dishes = BsonSerializer.Deserialize<List<Dish>>(result.ToJson());
            return dishes.FirstOrDefault();
            //return InMemoryDb.RetrieveDishByName(name);
        }


        public IMeal GetMealById(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("ID", id);
            var result = _mealsCollection.Find(filter).ToList();
            var meals = BsonSerializer.Deserialize<List<Meal>>(result.ToJson());
            return meals.FirstOrDefault();
            //return InMemoryDb.RetrieveMealById(id);
        }
        public IMeal GetMealByName(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
            var result = _mealsCollection.Find(filter).ToList();
            var meals = BsonSerializer.Deserialize<List<Meal>>(result.ToJson());
            return meals.FirstOrDefault();
            //return InMemoryDb.RetrieveMealByName(name);
        }

        public int DeleteDishById(int id)
        {
            if(false == IsDishExist(id)) return (int)ApiStatusCode.DishOrMealNotFound;

            var updateAppetizerFilter = Builders<BsonDocument>.Filter.Eq("Appetizer", id);
            var appetizerUpdate = Builders<BsonDocument>.Update.Set("Appetizer", BsonNull.Value);
            _mealsCollection.UpdateMany(updateAppetizerFilter, appetizerUpdate);

            var updateMainFilter = Builders<BsonDocument>.Filter.Eq("Main", id);
            var mainUpdate = Builders<BsonDocument>.Update.Set("Main", BsonNull.Value);
            _mealsCollection.UpdateMany(updateMainFilter, mainUpdate);

            var updateDessertFilter = Builders<BsonDocument>.Filter.Eq("Dessert", id);
            var dessertUpdate = Builders<BsonDocument>.Update.Set("Dessert", BsonNull.Value);
            _mealsCollection.UpdateMany(updateDessertFilter, dessertUpdate);

            InMemoryDb.UpdateDeletedDishInMealsStorage(id);


            var deleteFilter = Builders<BsonDocument>.Filter.Eq("ID", id);
            _dishesCollection.DeleteOne(deleteFilter);
            
            return id ;
            //return InMemoryDb.DeleteDish(id); ;
        }
        public int DeleteMealById(int id)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("ID", id);
            _mealsCollection.DeleteOne(deleteFilter);

            var deletedId = InMemoryDb.DeleteMeal(id);
            return deletedId;
        }

        public bool IsDishExist(string name)
        {
            return GetDishByName(name) != null;
        }

        public bool IsDishExist(int id)
        {
            return GetDishById(id) != null;
        }

        public bool IsMealExist(string name)
        {
            return GetMealByName(name) != null;
        }

        public bool IsMealExist(int id)
        {
            return GetMealById(id) != null;
        }

        public int UpdateMealById(int id, Meal meal)
        {
            if(GetDishById(id) == null) return (int)ApiStatusCode.DishOrMealNotFound;
            
            var updateDessertFilter = Builders<BsonDocument>.Filter.Eq("ID", id);
            _mealsCollection.ReplaceOne(updateDessertFilter, meal.ToBsonDocument());
            
            return id;
            //return InMemoryDb.UpdateMealById(id, meal);
        }
    }

    public class IdCounter
    {
        public string Id { get; set; }
        public int Value { get; set; }
    }
}
