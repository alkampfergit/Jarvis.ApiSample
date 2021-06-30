using Microsoft.AspNetCore.Mvc;

namespace Jarvis.HostApiVersion3.Controllers
{
    public partial class Document1Controller : Controller
    {
        [HttpGet]
        [Route("{documentId}")]
        public virtual IActionResult Get(string documentId)
        {
            return Ok(new
            {
                Id = documentId,
                Title = "This is a simulated title"
            });
        }
    }
}
