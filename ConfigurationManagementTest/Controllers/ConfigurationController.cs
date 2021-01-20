using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigurationManagementTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IOptions<Credentials> credentials;

        public ConfigurationController(IConfiguration configuration, IOptions<Credentials> credentials)
        {
            this.configuration = configuration;
            this.credentials = credentials;
        }

        [HttpPost, Route("{key}/{value}")]
        public IActionResult Post(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value);
            return Ok($"{key}:{value}");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"{credentials.Value.UserName} : {credentials.Value.Password}");
        }

        [HttpGet, Route("{key}")]
        public IActionResult Get(string key)
        {
            var value = configuration.GetValue<string>(key);
            return Ok(value);
        }
    }
}
