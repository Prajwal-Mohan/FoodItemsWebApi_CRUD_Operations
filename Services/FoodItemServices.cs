﻿using AutoMapper;
using Dapper;
using FoodItemsWebApi.Database_Services;
using FoodItemsWebApi.DTOs.FoodItem;
using FoodItemsWebApi.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi.Services
{
    public class FoodItemServices : IFoodItemServices
    {
        public FoodItemServices(IMapper mapper, IConfiguration configuration)
        {
            this._autoMapper = mapper;
            this._configuration = configuration;
        }



        public static List<FoodItem> FoodItems = new List<FoodItem> {
            new FoodItem {foodName = "Idli", price = 20 } ,
            new FoodItem {ID=1, foodName = "Dosa", price = 40 }
        };

        private readonly IMapper _autoMapper;
        private readonly IConfiguration _configuration;
        string ConnectionString => _configuration.GetConnectionString("localDB");

        public async Task<ServiceResponse<List<GetFoodItemDto>>> GetAllFoodItems()
        {
            ServiceResponse<List<GetFoodItemDto>> serviceResponse = new ServiceResponse<List<GetFoodItemDto>>();

            var data = await DataAccess.GetFoodItemDtos(ConnectionString);

            serviceResponse.Data = data;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFoodItemDto>> GetFoodItemById(int id)
        {
            ServiceResponse<GetFoodItemDto> serviceResponse = new ServiceResponse<GetFoodItemDto>();
            var data = await DataAccess.GetFoodItemDtosById(ConnectionString, id, new { ID = id});
            if (data == null )
            {
                serviceResponse.Data = null;
                serviceResponse.message = $"Can't Find Item with ID {id}";
                return serviceResponse;
            }

            serviceResponse.Data = data;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFoodItemDto>>> AddFoodItem(AddFoodItemDto newItem)
        {
            ServiceResponse<List<GetFoodItemDto>> serviceResponse = new ServiceResponse<List<GetFoodItemDto>>();
            int insertedRow = await DataAccess.AddFoodItems(ConnectionString, newItem, new { foodName = newItem.foodName, price = newItem.price });

            if (insertedRow > 0)
            {
                serviceResponse.Data = new List<GetFoodItemDto>() { new GetFoodItemDto { foodName = newItem.foodName, price = newItem.price } };
                serviceResponse.message = $"Rows Affected {insertedRow}";

                return serviceResponse;
            }
            serviceResponse.Data = null;
            serviceResponse.success = false;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFoodItemDto>> UpdateFoodItemById(UpdateFoodItemDto updatedItem)
        {
            ServiceResponse<GetFoodItemDto> serviceResponse = new ServiceResponse<GetFoodItemDto>();

            var data = await DataAccess.UpdateFoodItems(ConnectionString, updatedItem, new { foodName = updatedItem.foodName, price = updatedItem.price, id = updatedItem.ID});

            if (data == null)
            {
                serviceResponse.Data = null;
                serviceResponse.message = "Nothing's there in ID = " + updatedItem.ID;
                serviceResponse.success = false;
                return serviceResponse;
            }
            serviceResponse.Data = _autoMapper.Map<GetFoodItemDto>(data);
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<int>> DeleteFoodItemById(int id)
        {
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();

            var affectedRow = await DataAccess.RemoveFoodItems(ConnectionString, new { ID = id });
            if (affectedRow == 0)
            {
                serviceResponse.Data = 0;
                serviceResponse.message = "Item with id Not found and Can't Removed...  :( ";
                serviceResponse.success = false;
                return serviceResponse;
            }
            serviceResponse.message = "Deleted Item";

            return serviceResponse;
        }

    }
}
