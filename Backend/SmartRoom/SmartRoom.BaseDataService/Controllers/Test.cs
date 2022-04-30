using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartRoom.BaseDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Test";
        }
    }
}
