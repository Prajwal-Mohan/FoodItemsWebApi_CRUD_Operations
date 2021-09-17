using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FoodItemsWebApi.DTOs.FoodItem;
using MySqlConnector;

namespace FoodItemsWebApi.Database_Services
{
    public class FoodItemsDatabaseAccess
    {
        public static async Task<List<GetFoodItemDto>> GetAllFoodItemDtos(string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "call crud_operations.`crud_operations.spGetAllFoodItems`();";
                var data = await connection.QueryAsync<GetFoodItemDto>(sqlQuery);
                var listData = data.ToList();
                return listData;
            }
        }
        
        public static async Task<GetFoodItemDto> GetFoodItemDtosById<T>(string connectionString, int id, T param)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"call crud_operations.`crud_operations.spGetFoodItemByID`(@ID);";
                var data = await connection.QueryFirstOrDefaultAsync<GetFoodItemDto>(sqlQuery, param);
                return data;
            }
        }

        public static async Task<int> AddFoodItems<T>(string connectionString, AddFoodItemDto addFoodItemDto, T paramerters)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"call crud_operations.`crud_operations.spAddFoodItems`(@foodName, @price);";
                var rowsAffected = await connection.ExecuteAsync(sqlQuery, paramerters);
                return rowsAffected;
            }
        }
        
        public static async Task<UpdateFoodItemDto> UpdateFoodItems<T>(string connectionString, UpdateFoodItemDto addFoodItemDto, T paramerters)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //string sqlQuery = $"UPDATE food_items SET `foodName` = @foodName, `price` = @price WHERE (`id` = @id);";
                string sqlQuery = $"call crud_operations.`crud_operations.spUpdateItemByID`(@foodName, @price, @id);";

                var rowsAffected = await connection.ExecuteAsync(sqlQuery, paramerters);
                if (rowsAffected == 0)
                {
                    return null;
                }
                return addFoodItemDto;
            }
        }

        public static async Task<int> RemoveFoodItems<T>(string connectionString, T paramerters)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //string sqlQuery = $"DELETE FROM food_items WHERE (`id` = @ID);";
                string sqlQuery = $"call crud_operations.`crud_operations.spDeleteItemByID`(@ID);";
                var rowsAffected = await connection.ExecuteAsync(sqlQuery, paramerters);
                
                return rowsAffected;
            }
        }
    }
}
