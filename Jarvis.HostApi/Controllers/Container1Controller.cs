using Microsoft.AspNetCore.Mvc;

namespace Jarvis.HostApiVersion3.Controllers
{
    public partial class Container1Controller : Controller
    {
        [HttpGet]
        [Route("{containerId}")]
        public virtual IActionResult Get(string containerId)
        {
            return Ok(new
            {
                Id = containerId,
                Title = "root/bla/foo/bar"
            });
        }
    }
}
