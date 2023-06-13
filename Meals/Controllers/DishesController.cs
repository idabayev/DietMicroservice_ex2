using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ex1_ApiForMeals.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishesBusinessComponent _dishesBusinessComponent;
        private readonly ILogger<DishesController> _logger;

        public DishesController(ILogger<DishesController> logger)
        {
            _logger = logger;
            _dishesBusinessComponent = new DishesBusinessComponent();
        }

        //[HttpGet]
        //public SortedDictionary<int, DishDto> Get()
        //{
        //    return _dishesBusinessComponent.GetAllDishes();
        //}

        [HttpGet]
        public List<DishDto> Get()
        {
            return _dishesBusinessComponent.GetAllDishes();
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var dish = _dishesBusinessComponent.GetDishById(id);
            if(dish == null) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, dish);
        }
                
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var dish = _dishesBusinessComponent.GetDishByName(name);
            if (dish == null) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, dish);
        }

        #region Code for multiple dishes

        //[HttpPost]
        //public IActionResult Post(NameRoutingObject name)
        //{
        //    //request name in body is not in the correct form
        //    if (name.Name == null) return StatusCode((int)HttpStatusCode.UnprocessableEntity, -1);

        //    //request contenttype is not application/json
        //    if (Request.ContentType != "application/json") return StatusCode((int)HttpStatusCode.UnsupportedMediaType, 0);

        //    var result = _dishesBusinessComponent.AddDishToDb(name.Name);
        //    if (result.Count == 1)
        //    {
        //        switch (result.First())
        //        {
        //            //dish of given name already exists
        //            case (int)ApiStatusCode.DishOrMealAlreadyExist:
        //                return StatusCode((int)HttpStatusCode.UnprocessableEntity, -2);

        //            //Api ninjas does not recognize dish name
        //            case (int)ApiStatusCode.DishNotFountInNinjasApi:
        //                return StatusCode((int)HttpStatusCode.UnprocessableEntity, -3);

        //            //Api ninjas was not reachable
        //            case (int)ApiStatusCode.ApiNinjasNotReachable:
        //                return StatusCode((int)HttpStatusCode.GatewayTimeout, -4);

        //            default:
        //                return Created("~/Dishes", result.First());
        //        }
        //    }

        //    // In case name has more than one dish
        //    return Created("~/Dishes", result);
        //}

        #endregion

        #region Code for merged dishes

        [HttpPost]
        public IActionResult Post(NameRoutingObject name)
        {
            //request name in body is not in the correct form
            if (name.Name == null) return StatusCode((int)HttpStatusCode.UnprocessableEntity, -1);

            //request contenttype is not application/json
            if (Request.ContentType != "application/json") return StatusCode((int)HttpStatusCode.UnsupportedMediaType, 0);

            var result = _dishesBusinessComponent.AddDishToDb(name.Name);
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

                default:
                    return Created("~/Dishes", result);
            }
        }

        #endregion



        [HttpDelete]
        public IActionResult Delete()
        {
            return StatusCode((int)HttpStatusCode.MethodNotAllowed,new {error = "This method is not allowed for the requested URL" });
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _dishesBusinessComponent.DeleteDishById(id);
            if(result == (int)ApiStatusCode.DishOrMealNotFound) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK,result);
 
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var result = _dishesBusinessComponent.DeleteDishByName(name);
            if (result == (int)ApiStatusCode.DishOrMealNotFound) return StatusCode((int)HttpStatusCode.NotFound, -5);

            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}
