using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi.DTOs.FoodItem
{
    public class UpdateFoodItemDto
    {
        public int ID { get; set; } = 0;
        public string foodName { get; set; }
        public int price { get; set; }
    }
}
