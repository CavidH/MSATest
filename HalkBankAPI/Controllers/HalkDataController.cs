using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HalkBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HalkDataController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "HalkData", "value1", "value2" };
        }

    }
}
