using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaultSharp;

namespace HashiCorpVualtWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IVaultClient _vaultClient;

        public ConfigController(IVaultClient vaultClient)
        {
            _vaultClient = vaultClient;
        }

        [HttpGet("config")]
        public async Task<IActionResult> GetConfig()
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("secret/myapp/config");
            var username = secret.Data.Data["username"];
            var password = secret.Data.Data["password"];

            return Ok(new { username, password });
        }
    }
}
