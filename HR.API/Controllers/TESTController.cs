using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TESTController : ControllerBase
    {
        [Authorize]
        [HttpGet("TestAuthorization")]
        public ActionResult TestMe ()
        {
            return Ok("Success");
        }
    }
}
