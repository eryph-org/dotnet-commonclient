using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haipa.ClientRuntime.Authentication;
using Haipa.IdentityModel.Clients;
using JetBrains.Annotations;
using Microsoft.Rest;

namespace Haipa.CommonClient.Commands
{
    public class ServiceClientCredentialsCache
    {
        public static readonly ServiceClientCredentialsCache Instance = new ServiceClientCredentialsCache();

        private readonly  ConcurrentDictionary<string, ServiceClientCredentials> _cache =
            new ConcurrentDictionary<string, ServiceClientCredentials>();

        public async Task<ServiceClientCredentials> GetServiceCredentials([NotNull] ClientCredentials credentials, IEnumerable<string> scopes = null)
        {
            var scopesArray = scopes == null ? new string[0] : scopes.ToArray();
            var key = $"{credentials.Id}_{credentials.IdentityProvider}_scopes:{string.Join(",", scopesArray)}";

            if (_cache.ContainsKey(key))
                return _cache[key];


            var serviceClientCredentials = await ApplicationTokenProvider.LogonWithHaipaClient(credentials, scopesArray).ConfigureAwait(false);
            return _cache.GetOrAdd(key, serviceClientCredentials);
        }
    }
}