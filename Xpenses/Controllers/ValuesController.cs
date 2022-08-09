using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xpenses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ValuesController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<string> GetAppSettings()
        {
            return _config["EnvironmentName"];
        }
    }
}
