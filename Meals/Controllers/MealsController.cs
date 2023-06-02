using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ex1_ApiForMeals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsBusinessComponent _mealsBusinessComponent;
        private readonly ILogger<DishesController> _logger;

        public MealsController(ILogger<DishesController> logger)
        {
            _logger = logger;
            _mealsBusinessComponent = new MealsBusinessComponent();
        }

        [HttpGet]
        public SortedDictionary<int, MealDto> Get()
        {
            return _mealsBusinessComponent.GetAllMeals();
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var meal = _mealsBusinessComponent.GetMealById(id);
            if (meal == null) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, meal);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var meal = _mealsBusinessComponent.GetMealByName(name);
            if (meal == null) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, meal);
        }


        [HttpPost]
        public IActionResult Post(MealRoutingObject meal)
        {
            //request keys in body is not in the correct form
            if (false == AreRequestsParametersValid(meal)) return StatusCode((int)HttpStatusCode.UnprocessableEntity, -1);

            //request contenttype is not application/json
            if (Request.ContentType != "application/json") return StatusCode((int)HttpStatusCode.UnsupportedMediaType, 0);

            var result = _mealsBusinessComponent.AddMealToDb(meal);
            switch (result)
            {
                //dish of given name already exists
                case (int)ApiStatusCode.DishOrMealAlreadyExist:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -2);

                //Api ninjas does not recognize dish name
                case (int)ApiStatusCode.DishNotFountInNinjasApi:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -3);

                //Api ninjas was not reachable
                case (int)ApiStatusCode.ApiNinjasNotReachable:
                    return StatusCode((int)HttpStatusCode.GatewayTimeout, -4);
                    
                //Api ninjas was not reachable
                case (int)ApiStatusCode.MealsDishNotExist:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -6);

                default:
                    return Created("~/Meals", result);
            }
        }

        private bool AreRequestsParametersValid(MealRoutingObject meal)
        {
            if (meal.Name == null || meal.Appetizer == null || meal.Main == null || meal.Dessert == null) return false;

            return true;
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return StatusCode((int)HttpStatusCode.MethodNotAllowed, new { error = "This method is not allowed for the requested URL" });
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _mealsBusinessComponent.DeleteMealById(id);
            if (result == (int)ApiStatusCode.DishOrMealNotFound) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, result);

        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var result = _mealsBusinessComponent.DeleteMealByName(name);
            if (result == (int)ApiStatusCode.DishOrMealNotFound) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, MealRoutingObject meal)
        {
            var result = _mealsBusinessComponent.UpdateMealById(id, meal);
            switch (result)
            {
                //dish of given name already exists
                case (int)ApiStatusCode.DishOrMealAlreadyExist:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -2);

                //Api ninjas does not recognize dish name
                case (int)ApiStatusCode.DishNotFountInNinjasApi:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -3);

                //Api ninjas was not reachable
                case (int)ApiStatusCode.ApiNinjasNotReachable:
                    return StatusCode((int)HttpStatusCode.GatewayTimeout, -4);

                //Api ninjas was not reachable
                case (int)ApiStatusCode.MealsDishNotExist:
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, -6);

                default:
                    return StatusCode((int)HttpStatusCode.OK, result);
            }            
        }
    }
}
