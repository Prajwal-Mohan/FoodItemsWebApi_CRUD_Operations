using AutoMapper;
using FoodItemsWebApi.DTOs.FoodItem;
using FoodItemsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodItemsWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FoodItem, GetFoodItemDto>();
            CreateMap<GetFoodItemDto, FoodItem>();
            CreateMap<UpdateFoodItemDto, GetFoodItemDto>();
            CreateMap<AddFoodItemDto, FoodItem>();
            CreateMap<FoodItem, UpdateFoodItemDto>();
        }
    }
}
