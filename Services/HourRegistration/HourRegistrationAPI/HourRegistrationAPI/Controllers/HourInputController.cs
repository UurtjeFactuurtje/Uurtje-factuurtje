using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HourRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HourInputController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> PostHours()
        {
            return new string[] { "It works"};
        }

    }
}