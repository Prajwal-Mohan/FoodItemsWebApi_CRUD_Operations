using AutoMapper;
using FoodItemsWebApi.DTOs.FoodItem;
using FoodItemsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi.Services
{
    public class FoodItemServices : IFoodItemServices
    {

        public FoodItemServices(IMapper mapper)
        {
            this._autoMapper = mapper;
        }

        public static List<FoodItem> FoodItems = new List<FoodItem> {
            new FoodItem {foodName = "Idli", price = 20 } ,
            new FoodItem {ID=1, foodName = "Dosa", price = 40 }
        };

        private readonly IMapper _autoMapper;

        public async Task<ServiceResponse<List<GetFoodItemDto>>> GetAllFoodItems()
        {
            ServiceResponse<List<GetFoodItemDto>> serviceResponse = new ServiceResponse<List<GetFoodItemDto>>();
            serviceResponse.Data = FoodItems.Select(item => _autoMapper.Map<GetFoodItemDto>(item)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFoodItemDto>> GetFoodItemById(int id)
        {
            ServiceResponse<GetFoodItemDto> serviceResponse = new ServiceResponse<GetFoodItemDto>();
            serviceResponse.Data = _autoMapper.Map<GetFoodItemDto>(FoodItems.FirstOrDefault(i => i.ID == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFoodItemDto>>> AddFoodItem(AddFoodItemDto newItem)
        {
            //int id = FoodItems.Max(oldItem => oldItem.ID) + 1;
            //newItem.ID = id;

            ServiceResponse<List<GetFoodItemDto>> serviceResponse = new ServiceResponse<List<GetFoodItemDto>>();
            FoodItem newFoodItem = _autoMapper.Map<FoodItem>(newItem);
            newFoodItem.ID = FoodItems.Max(i => i.ID) + 1;
            FoodItems.Add(_autoMapper.Map<FoodItem>(newFoodItem));
            serviceResponse.Data = ( FoodItems.Select(foodItem => _autoMapper.Map<GetFoodItemDto>(foodItem))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFoodItemDto>> UpdateFoodItemById(UpdateFoodItemDto updatedItem)
        {
            ServiceResponse<GetFoodItemDto> serviceResponse = new ServiceResponse<GetFoodItemDto>();

            FoodItem foodItem = FoodItems.FirstOrDefault(food => food.ID == updatedItem.ID);
            if (foodItem == null)
            {
                serviceResponse.Data = null;
                serviceResponse.success = false;
                serviceResponse.message = $"Cant find Item with id =  {updatedItem.ID}";
                return serviceResponse;
            }

            foodItem.ID = updatedItem.ID;
            foodItem.foodName = updatedItem.foodName;
            foodItem.price = updatedItem.price;

            serviceResponse.Data = _autoMapper.Map<GetFoodItemDto>(foodItem);

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetFoodItemDto>> DeleteFoodItemById(int id)
        {
            ServiceResponse<GetFoodItemDto> serviceResponse = new ServiceResponse<GetFoodItemDto>();

            FoodItem foodItem = FoodItems.FirstOrDefault(itemId => id == itemId.ID);
            if (foodItem == null)
            {
                serviceResponse.Data = null;
                serviceResponse.message = "Item with id Not found...  :( ";
                serviceResponse.success = false;
                return serviceResponse;
            }

            FoodItems.RemoveAt(id);
            serviceResponse.Data = _autoMapper.Map<GetFoodItemDto>(foodItem);
            serviceResponse.message = "Removed Item";

            return serviceResponse;
        }
    }
}
