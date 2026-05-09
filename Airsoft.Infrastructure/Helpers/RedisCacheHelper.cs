using StackExchange.Redis;
using System.Text.Json;

namespace Airsoft.Infrastructure.Helpers
{
    public class RedisCacheHelper
    {
        private readonly IDatabase _db;

        public RedisCacheHelper(string connectionString)
        {
            var redis = ConnectionMultiplexer.Connect(connectionString);           
            _db = redis.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value);

            await _db.StringSetAsync(
                key,
                json,
                expiration ?? TimeSpan.FromMinutes(10)
            );
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            RedisValue value = await _db.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task<TimeSpan?> GetTTLAsync(string key)
        {
            return await _db.KeyTimeToLiveAsync(key);
        }

        public async Task RenewExpirationAsync(string key, TimeSpan expiration)
        {
            await _db.KeyExpireAsync(key, expiration);
        }

        public string GenerateKey(string prefix, string identifier)
        {
            return $"{prefix}:{identifier}";
        }
    }
}
