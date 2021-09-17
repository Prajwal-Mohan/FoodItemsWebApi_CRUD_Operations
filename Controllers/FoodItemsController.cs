using FoodItemsWebApi.DTOs.FoodItem;
using FoodItemsWebApi.Models;
using FoodItemsWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodItemsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodItemsController : Controller
    {
        private readonly IFoodItemServices _foodItemService;

        public FoodItemsController(IFoodItemServices foodItemService)
        {
            this._foodItemService = foodItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFoodItems()
        {
            return Ok(await _foodItemService.GetAllFoodItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodItemById(int id)
        {
            return Ok(await _foodItemService.GetFoodItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddFoodItem(AddFoodItemDto item)
        {
            return Ok(await _foodItemService.AddFoodItem(item));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateItem(UpdateFoodItemDto newItem)
        {

            return Ok(await _foodItemService.UpdateFoodItemById(newItem));
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            return Ok(await _foodItemService.DeleteFoodItemById(id));
        }
    }
}
