using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Models;
using StackExchange.Redis;

namespace Newtouch.Herp.API.Services
{
    /// <summary>
    /// Redis-backed token store mirroring ApiControllerBaseEx in the PDS source
    /// mirror of FrameworkBase.API: SaveIdentity mints a GUID token and stores
    /// the identity JSON for 20 minutes; GetIdentity refreshes the 20-minute
    /// expiry on every successful read. A Redis failure on read is treated as
    /// "token not found" (callers then report TOKEN_IS_EXPIRED, as the
    /// original does for an unknown token) instead of crashing the request.
    /// </summary>
    public interface ITokenStore
    {
        string SaveIdentity(Identity identity);
        UserIdentity GetIdentity(string token);
    }

    public class RedisTokenStore : ITokenStore
    {
        private static readonly TimeSpan TokenLifetime = new TimeSpan(0, 20, 0);
        private readonly Lazy<ConnectionMultiplexer> _redis;

        public RedisTokenStore(IConfiguration configuration)
        {
            var connection = configuration["RedisConfig:Connection"]
                ?? configuration["ConnectionStrings:RedisConfig:Connection"];
            _redis = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(connection ?? "127.0.0.1:6379"));
        }

        public string SaveIdentity(Identity identity)
        {
            var token = Guid.NewGuid().ToString();
            var db = _redis.Value.GetDatabase();
            db.StringSet(token, JsonConvert.SerializeObject(identity), TokenLifetime);
            return token;
        }

        public UserIdentity GetIdentity(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;
            try
            {
                var db = _redis.Value.GetDatabase();
                var value = db.StringGet(token);
                if (value.IsNullOrEmpty) return null;
                var identity = JsonConvert.DeserializeObject<UserIdentity>(value);
                db.KeyExpire(token, TokenLifetime);
                return identity;
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Port of Newtouch.Herp.API.App_Start.UserIdentityResolver: resolves a
    /// token through the token store. A token whose stored identity has a
    /// different TokenType than the request is a type mismatch.
    /// </summary>
    public class UserIdentityResolver : IUserIdentityResolver<Identity>
    {
        private readonly ITokenStore _tokenStore;

        public UserIdentityResolver(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public Identity GetIdentity(string token)
        {
            return _tokenStore.GetIdentity(token);
        }

        public Identity GetIdentity(string token, string tokenType)
        {
            var identity = _tokenStore.GetIdentity(token);
            if (identity != null
                && !string.IsNullOrWhiteSpace(tokenType)
                && !string.IsNullOrWhiteSpace(identity.TokenType)
                && !string.Equals(tokenType, identity.TokenType, StringComparison.Ordinal))
            {
                throw new TokenTypeNotMatchException();
            }
            return identity;
        }
    }
}
