using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Swapy.BLL.Interfaces;

namespace Swapy.BLL.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _client;

        public KeyVaultService(string keyVaultUrl) => _client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        public async Task<string> GetSecretValue(string secretName)
        {
            KeyVaultSecret secret = await _client.GetSecretAsync(secretName);
            return secret.Value;
        }
    }
}
