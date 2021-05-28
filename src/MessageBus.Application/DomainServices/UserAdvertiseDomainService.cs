using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBus.Application.DomainServices
{
    public class UserAdvertiseDomainService
    {
        private readonly string _connectionString;
        private readonly ILogger<UserAdvertiseDomainService> _logger;

        public UserAdvertiseDomainService(IConfiguration configuration, ILogger<UserAdvertiseDomainService> logger)
        {
            _connectionString = configuration.GetConnectionString("advertise");
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="status">"InProgress", "HasExpired", "Finished"</param>
        /// <returns></returns>
        public async Task<List<int>> FindAdvertiseIdsAsync(DateTime time, string status, int limit)
        {
            string cmdText = "SELECT `id` FROM `user_ad_tasks` WHERE `expired_time` < @Time AND `status` =  @Status LIMIT @Limit;";
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new MySqlCommand(cmdText, connection);

            command.Parameters.AddWithValue("@Time", time);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@Limit", limit);

            using var reader = command.ExecuteReader();
            List<int> ids = new();
            while (reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }
            await reader.CloseAsync();
            await connection.CloseAsync();
            return ids;
        }

        public async Task UpdateAdvertiseToExpiredAsync(params int[] ids)
        {
            string idString = string.Join(',', ids);
            _logger.LogInformation("Update advertise to expired: {0}", idString);
            string cmdText = $"DELETE FROM `user_ad_task_log` WHERE `user_ad_task_id` IN ({idString});UPDATE `user_ad_tasks` SET `status`='HasExpired' WHERE `id` IN({idString});";
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                using var command = new MySqlCommand(cmdText, connection, transaction);
                command.ExecuteNonQuery();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "UpdateAdvertiseToExpiredAsync: ids({0}, {1})", idString, ex.Message);
            }
            await connection.CloseAsync();
        }

        public async Task RemoveAdvertiseAsync(params int[] ids)
        {
            string idString = string.Join(',', ids);
            _logger.LogInformation("Remove advertise: {0}", idString);
            string cmdText = $"DELETE FROM `user_ad_task_log` WHERE `user_ad_task_id` = @Id;DELETE `user_ad_tasks` WHERE `id` IN({idString});";
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                using var command = new MySqlCommand(cmdText, connection, transaction);
                command.ExecuteNonQuery();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
            await connection.CloseAsync();
        }
    }
}
