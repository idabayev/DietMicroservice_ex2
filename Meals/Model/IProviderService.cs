using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ex1_ApiForMeals
{
    public interface IProviderService
    {
        Task<List<Dish>> GetDishInfoFromExternalApiAsync(string name);
    }
}
