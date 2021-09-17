using FoodItemsWebApi.DTOs.FoodItem;
using FoodItemsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi.Services
{
    public interface IFoodItemServices
    {
        Task<ServiceResponse<List<GetFoodItemDto>>> GetAllFoodItems();
        Task<ServiceResponse<GetFoodItemDto>> GetFoodItemById(int id);
        Task<ServiceResponse<List<GetFoodItemDto>>> AddFoodItem(AddFoodItemDto newItem);
        Task<ServiceResponse<GetFoodItemDto>> UpdateFoodItemById(UpdateFoodItemDto updatedItem);
        Task<ServiceResponse<GetFoodItemDto>> DeleteFoodItemById(int id);
    }
}
