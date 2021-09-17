using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi.DTOs.FoodItem
{
    public class AddFoodItemDto
    {
        public string foodName { get; set; }
        public int price { get; set; }
    }
}
